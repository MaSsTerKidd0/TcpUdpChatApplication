using ChatApp.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ChatApp.ViewModels
{
    public class GroupsWindowViewModel : ObservableObject
    {
        #region Fields
        public string _newGroupChatName;
        public GroupChat _selectedGroupChat;
        public static ObservableCollection<GroupChat> _availableGroupChats;
        #endregion
        #region Properties
        public RelayCommand JoinGroupChatCommand { get; private set; }
        public RelayCommand CreateGroupChatCommand { get; private set; }

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
            get { return _availableGroupChats;}
            set { SetProperty(ref _availableGroupChats, value); }
        }
        #endregion

        public GroupsWindowViewModel()
        {
            InitCommands();
        }
        public void InitCommands() 
        {
            JoinGroupChatCommand = new RelayCommand(JoinGroupChat);
            CreateGroupChatCommand = new RelayCommand(CreateGroupChat);
            AvailableGroupChats = new ObservableCollection<GroupChat>();
        }

        public void CreateGroupChat()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ClientInfo.Instance.Client.SendCreateGroupRequest(NewGroupChatName);
                GroupChat newGroupChat = new GroupChat (NewGroupChatName);
                //AvailableGroupChats.Add(newGroupChat);
            });
        }

        private void JoinGroupChat()
        {
            ClientInfo.Instance.Client.SendJoinGroupRequest(SelectedGroupChat.GroupName);
            MessageBox.Show(SelectedGroupChat.GroupName);
        }
    }
}
