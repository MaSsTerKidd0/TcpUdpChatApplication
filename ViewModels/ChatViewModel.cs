using ChatApp.Enums;
using ChatApp.Models;
using ChatApp.ResponseCommand;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
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
        public static Dictionary<int, IResponseCommand> ResponseCommandDictionary = new Dictionary<int, IResponseCommand>();
        private string msgToSend;
        private string _newGroupChatName;
        private GroupChat _selectedGroupChat;
        private static ObservableCollection<GroupChat> _availableGroupChats;
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

        #region Init
        public ChatViewModel()
        {
            initVariables();
            initCommands();
            initResponsesCommands();
        }

        private void initVariables()
        {
            _availableClients = new ObservableCollection<string>();
            _messages = new ObservableCollection<Message>();
        }

        private void initCommands()
        {
            SendCommand = new RelayCommand(SendMessage);
            LoadChatCommand = new RelayCommand(ReLoadChat);
            JoinGroupChatCommand = new RelayCommand(JoinGroupChat);
            CreateGroupChatCommand = new RelayCommand(CreateGroupChat);
            AvailableGroupChats = new ObservableCollection<GroupChat>();
        }

        private void initResponsesCommands()
        {
            ResponseCommandDictionary.Add((int)ResponsesEnum.LoadAvailableChats, new LoadAvaiableChatsCommand(AvailableChats));
            ResponseCommandDictionary.Add((int)ResponsesEnum.MessageReceived, new MessageReceivedCommand(Messages));
            ResponseCommandDictionary.Add((int)ResponsesEnum.LoadJoinedGroupChat, new LoadJoinedGroupChatsCommand(AvailableChats));
            ResponseCommandDictionary.Add((int)ResponsesEnum.LoadAvailableGroupChats, new LoadAvailableGroupChatsCommand(AvailableGroupChats));
        }
        #endregion

        public static void HandleServerResponse(string response)
        {
            int responseType;
            int.TryParse(response.Split('#')[0], out responseType);
            if (ResponseCommandDictionary.ContainsKey(responseType))
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    ResponseCommandDictionary[responseType].Execute(response);
                });
            }
        }
        private void SendMessage()
        {
            ClientInfo.Instance.Client.SendMessageRequest(SelectedAvailableChat, MessageToSend);
        }

        private void ReLoadChat()
        {
            Application.Current?.Dispatcher.Invoke(() =>
            {
                if (SelectedAvailableChat != null && ClientInfo.Instance.ClientChats.ContainsKey(SelectedAvailableChat))
                {
                    Messages = ClientInfo.Instance.ClientChats[SelectedAvailableChat].Messages;
                }
                if (SelectedAvailableChat != null && !ClientInfo.Instance.ClientChats.ContainsKey(SelectedAvailableChat))
                {
                    Messages = null;
                }
            });
        }

        public  void CreateGroupChat()
        {
            ClientInfo.Instance.Client.SendCreateGroupRequest(NewGroupChatName);
        }
        private void JoinGroupChat()
        {
            ClientInfo.Instance.Client.SendJoinGroupRequest(SelectedGroupChat.GroupName);
            Application.Current.Dispatcher.Invoke(() => AvailableGroupChats.Remove(SelectedGroupChat));
        }

        public void ExitServer()
        {
            ClientInfo.Instance.Client.SendCloseConnectionRequest();
            ClientInfo.Instance.Client.CancelRecieveTask();
        }
    }
}
