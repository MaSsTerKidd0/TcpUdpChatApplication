using ChatApp.Models;
using System.Collections.ObjectModel;
using System.Linq;

namespace ChatApp.ResponseCommand
{
    public class LoadAvailableGroupChatsCommand : IResponseCommand
    {
        private ObservableCollection<GroupChat> _groupChats;

        public LoadAvailableGroupChatsCommand(ObservableCollection<GroupChat> groupChats)
        {
            _groupChats = groupChats;
        }

        public void Execute(string response)
        {
            string[] availableChatsNames = response.Split('#');
            for (int i = 1; i < availableChatsNames.Length; i++)
            {
                string availableChatName = availableChatsNames[i];
                if (!string.IsNullOrEmpty(availableChatName) &&
                    _groupChats.All(chat => chat.GroupName != availableChatName))
                {
                    _groupChats.Add(new GroupChat(availableChatName));
                }
            }
        }
    }
}
