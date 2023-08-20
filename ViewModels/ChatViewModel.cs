using ChatApp.Models;
using ChatApp.Views;
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
        private string _selectedAvailableClient;
        public static Dictionary<string, Action<string>> ResponseDictionary = new Dictionary<string, Action<string>>();
        private string msgToSend;

        #endregion

        #region Properties
        public RelayCommand SendCommand { get; private set; }
        public RelayCommand LoadChatCommand { get; private set; }
        public RelayCommand OpenGroupsWindowCommand { get; private set; }

        public ObservableCollection<string> AvailableClients
        {
            get { return _availableClients; }
            set { SetProperty(ref _availableClients, value); }
        }
        public string SelectedAvailableClient
        {
            get { return _selectedAvailableClient; }
            set { SetProperty(ref _selectedAvailableClient, value); }
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
            OpenGroupsWindowCommand = new RelayCommand(OpenGroupsWindow);
        }

       
        private void initResponseDictionary()
        {
            ResponseDictionary.Add("500", LoadAvailableClients);
            ResponseDictionary.Add("600", MessageReceived);
        }

        private void SendMessage()
        {
            ClientInfo.Instance.Client.SendMessageRequest(SelectedAvailableClient, MessageToSend);
        }

        public static void HandleServerResponse(string response)
        {
            string responseType = response.Split('#')[0];

            if (ResponseDictionary.ContainsKey(responseType))
            {
                ResponseDictionary[responseType](response);
            }
            else
            {
                MessageBox.Show("Wrong Server response");
            }
        }

        private void LoadAvailableClients(string serverResponse)
        {
            string[] connectedAccountsNames = serverResponse.Split('#');
            for (int i = 1; i < connectedAccountsNames.Length && (connectedAccountsNames.Length - 1) != AvailableClients.Count; i++)
            {
                if (ClientInfo.Instance.UserName != connectedAccountsNames[i] && !AvailableClients.Contains(connectedAccountsNames[i]))
                {
                    Application.Current.Dispatcher.Invoke(() =>
                    {
                        AvailableClients.Add(connectedAccountsNames[i]);
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
        private void ReLoadChat()
        {
            if (SelectedAvailableClient != null && ClientInfo.Instance.ClientChats.ContainsKey(SelectedAvailableClient))
                Messages = ClientInfo.Instance.ClientChats[SelectedAvailableClient].Messages;
        }

        private void CreateChat(string clientName)
        {
            if (!ClientInfo.Instance.ClientChats.ContainsKey(clientName))
            {
                ClientInfo.Instance.ClientChats.Add(clientName, new Chat());
            }
        }
        private void OpenGroupsWindow()
        {
            AvailableGroups availableGroups = new AvailableGroups();
            availableGroups.Show();
        }

    }
}
