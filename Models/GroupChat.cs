using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.Models
{
    public class GroupChat
    {
        private string _chatName;
        private static int _inChatClientAmount;

        public string GroupName
        {
            get { return _chatName; }
            set { _chatName = value; }
        }
        public int InChatClientAmount
        {
            get { return _inChatClientAmount; }
        }

        public GroupChat(string availableChatName)
        {
            GroupName = availableChatName;
            //_inChatClientAmount++; After Join
        }

        
    }
}
