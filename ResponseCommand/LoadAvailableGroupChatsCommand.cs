using ChatApp.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChatApp.ResponseCommand
{
    public class LoadAvailableGroupChatsCommand : ICommand
    {
        private readonly string _response;

        public LoadAvailableGroupChatsCommand(string response)
        {
            _response = response;
        }

        public void Execute(ObservableCollection<InterfaceExample> example)
        {
            List<InterfaceExample> interfaceList = new List<InterfaceExample>(example);
            List<GroupChat> groupChatList = interfaceList.OfType<GroupChat>().ToList();
            ObservableCollection<GroupChat> observableGroupChatList = new ObservableCollection<GroupChat>(groupChatList);

            string[] availableChatsNames = _response.Split('#');
            for (int i = 1; i < availableChatsNames.Length; i++)
            {
                string availableChatName = availableChatsNames[i];
                if (!string.IsNullOrEmpty(availableChatName) &&
                    observableGroupChatList.All(chat => chat.GroupName != availableChatName))
                {
                    observableGroupChatList.Add(new GroupChat(availableChatName));
                }
            }
            //example = observableGroupChatList;
        }
    }

}
