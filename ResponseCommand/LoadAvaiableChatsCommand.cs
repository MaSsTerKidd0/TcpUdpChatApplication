using ChatApp.Models;
using System.Collections.ObjectModel;

namespace ChatApp.ResponseCommand
{
    public class LoadAvaiableChatsCommand : IResponseCommand
    {
        ObservableCollection<string> _availableChats;
        public LoadAvaiableChatsCommand(ObservableCollection<string> availableChats)
        {
            _availableChats = availableChats;
        }

        public void Execute(string response)
        {

            string[] connectedAccountsNames = response.Split('#');
            for (int i = 1; i < connectedAccountsNames.Length && (connectedAccountsNames.Length - 1) != _availableChats.Count; i++)
            {
                if (ClientInfo.Instance.UserName != connectedAccountsNames[i] && !_availableChats.Contains(connectedAccountsNames[i]))
                {
                    _availableChats.Add(connectedAccountsNames[i]);
                }
            }
        }
    }
}







