﻿namespace ChatApp.Models
{
    public class GroupChat
    {
        private string _chatName;

        public string GroupName
        {
            get { return _chatName; }
            set { _chatName = value; }
        }

        public GroupChat(string availableChatName)
        {
            GroupName = availableChatName;
        }
    }
}
