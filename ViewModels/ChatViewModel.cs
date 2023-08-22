﻿using ChatApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Media;

namespace ChatApp.ViewModels
{
    public class ChatViewModel : ObservableObject
    {
        #region Fields
        private ObservableCollection<string> _availableClients;
        ObservableCollection<Message> _messages;
        private string _selectedAvailableChat;
        public static Dictionary<int, Action<string>> ResponseDictionary = new Dictionary<int, Action<string>>();
        private string msgToSend;
        private string _newGroupChatName;
        private GroupChat _selectedGroupChat;
        private ObservableCollection<GroupChat> _availableGroupChats;
        #endregion

        #region Properties
        public RelayCommand SendCommand { get; private set; }
        public RelayCommand LoadChatCommand { get; private set; }
        public RelayCommand JoinGroupChatCommand { get; private set; }
        public RelayCommand CreateGroupChatCommand { get; private set; }

        public ObservableCollection<string> AvailableChats
        {
            get { return _availableClients; }
            set { SetProperty(ref _availableClients, value); }
        }
        public string SelectedAvailableChat
        {
            get { return _selectedAvailableChat; }
            set { SetProperty(ref _selectedAvailableChat, value); }
        }

        public string MessageToSend
        {
            get { return msgToSend; }
            set { SetProperty(ref msgToSend, value); }
        }
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
            set { SetProperty(ref _messages, value); }
        }
        public string NewGroupChatName
        {
            get { return _newGroupChatName; }
            set { SetProperty(ref _newGroupChatName, value); }
        }
        public GroupChat SelectedGroupChat
        {
            get { return _selectedGroupChat; }
            set { SetProperty(ref _selectedGroupChat, value); }
        }
        public ObservableCollection<GroupChat> AvailableGroupChats
        {
            get { return _availableGroupChats; }
            set { SetProperty(ref _availableGroupChats, value); }
        }
        #endregion

        public ChatViewModel()
        {
            _availableClients = new ObservableCollection<string>();
            _messages = new ObservableCollection<Message>();
            initCommands();
            initResponseDictionary();
        }

        private void initCommands()
        {
            SendCommand = new RelayCommand(SendMessage);
            LoadChatCommand = new RelayCommand(ReLoadChat);
            JoinGroupChatCommand = new RelayCommand(JoinGroupChat);
            CreateGroupChatCommand = new RelayCommand(CreateGroupChat);
            AvailableGroupChats = new ObservableCollection<GroupChat>();
        }

        private void initResponseDictionary()
        {
            ResponseDictionary.Add(500, LoadAvailableChats);
            ResponseDictionary.Add(600, MessageReceived);
            ResponseDictionary.Add(700, LoadJoinedGroupChat);
            ResponseDictionary.Add(800, LoadAvailableGroupChats);
        }


        public static void HandleServerResponse(string response)
        {
            int responseType = int.Parse(response.Split('#')[0]);

            if (ResponseDictionary.ContainsKey(responseType))
            {
                ResponseDictionary[responseType](response);
            }
            else
            {
                MessageBox.Show("Wrong Server response: " + response);
            }
        }

        private void LoadAvailableGroupChats(string response)
        {
            string[] AvailablesChatsNames = response.Split('#');
            for (int i = 1; i < AvailablesChatsNames.Length; i++)
            {
                string availableChatName = AvailablesChatsNames[i];
                if (!string.IsNullOrEmpty(availableChatName) &&
                    AvailableGroupChats.All(chat => chat.GroupName != availableChatName))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                        AvailableGroupChats.Add(new GroupChat(availableChatName)));
                }
            }
        }

        private void SendMessage()
        {
            ClientInfo.Instance.Client.SendMessageRequest(SelectedAvailableChat, MessageToSend);
        }
        private void LoadAvailableChats(string serverResponse)
        {
            string[] connectedAccountsNames = serverResponse.Split('#');
            for (int i = 1; i < connectedAccountsNames.Length && (connectedAccountsNames.Length - 1) != AvailableChats.Count; i++)
            {
                if (ClientInfo.Instance.UserName != connectedAccountsNames[i] && !AvailableChats.Contains(connectedAccountsNames[i]))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        
                        AvailableChats.Add(connectedAccountsNames[i]);
                    });
                }
            }
        }
        private void MessageReceived(string serverResponse)
        {
            string[] responseParts = serverResponse.Split('#');
            string from = responseParts[1];
            string to = responseParts[2];
            string message = responseParts[3];
            string dateTimeString = responseParts[4].ToString();

            CreateChat(from);
            CreateChat(to);

            Message newMessage = new Message
            {
                Sender = from == ClientInfo.Instance.UserName ? "Me" : from,
                MessageText = message,
                Background = (from == ClientInfo.Instance.UserName ? Colors.LightBlue.ToString() : Colors.LightGray.ToString()),
                TextColor = (from == ClientInfo.Instance.UserName ? Colors.Crimson.ToString() : Colors.Black.ToString()),
                Time = dateTimeString
            };

            Application.Current.Dispatcher.Invoke(() =>
            {
                ClientInfo.Instance.ClientChats[from].Messages.Add(newMessage);
                ClientInfo.Instance.ClientChats[to].Messages.Add(newMessage);
                Messages = new ObservableCollection<Message>(OrderMessagesByTime(to, from));
                ReLoadChat();
            });
        }

        public List<Message> OrderMessagesByTime(string to, string from)
        {
            List<Message> allMessages = new List<Message>();
            DateTime time;
            allMessages.ToList().AddRange(ClientInfo.Instance.ClientChats[from].Messages);
            allMessages.ToList().AddRange(ClientInfo.Instance.ClientChats[to].Messages);

            List<Message> orderedMessages = allMessages.OrderBy(message => DateTime.TryParse(message.Time, out time) ? time : DateTime.MinValue).ToList();

            return orderedMessages;
        }

        public void LoadJoinedGroupChat(string response)
        {
            string[] chatsNames = response.Split('#');

            for (int i = 1; i < chatsNames.Length && (chatsNames.Length - 1) != AvailableChats.Count; i++)
            {
                if (!(AvailableChats.Contains(chatsNames[i])))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        
                        AvailableChats.Add(chatsNames[i]);
                    });
                }
            }
        }

        private void ReLoadChat()
        {
            if (SelectedAvailableChat != null && ClientInfo.Instance.ClientChats.ContainsKey(SelectedAvailableChat))
                Messages = ClientInfo.Instance.ClientChats[SelectedAvailableChat].Messages;
        }

        private void CreateChat(string clientName)
        {
            if (!ClientInfo.Instance.ClientChats.ContainsKey(clientName))
            {
                ClientInfo.Instance.ClientChats.Add(clientName, new Chat());
            }
        }
        public void CreateGroupChat()
        {
            ClientInfo.Instance.Client.SendCreateGroupRequest(NewGroupChatName);
        }

        private void JoinGroupChat()
        {
            ClientInfo.Instance.Client.SendJoinGroupRequest(SelectedGroupChat.GroupName);
            Application.Current.Dispatcher.Invoke(() => AvailableGroupChats.Remove(SelectedGroupChat));
        }
    }
}
