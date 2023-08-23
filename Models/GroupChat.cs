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
