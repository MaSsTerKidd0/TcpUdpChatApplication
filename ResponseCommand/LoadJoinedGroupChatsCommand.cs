using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatApp.ResponseCommand
{
    public class LoadJoinedGroupChatsCommand : IResponseCommand
    {
        private ObservableCollection<string>  _availableChats;
        public LoadJoinedGroupChatsCommand(ObservableCollection<string> availableChats) 
        {
            _availableChats = availableChats;
        }
        public void Execute(string response)
        {
            string[] chatsNames = response.Split('#');

            for (int i = 1; i < chatsNames.Length && (chatsNames.Length - 1) != _availableChats.Count; i++)
            {
                if (!(_availableChats.Contains(chatsNames[i])))
                {
                    _availableChats.Add(chatsNames[i]);
                }
            }
        }
    }
}
