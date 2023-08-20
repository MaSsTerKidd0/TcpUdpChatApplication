using System.Collections.ObjectModel;

namespace ChatApp.Models
{
    public class Chat
    {
        #region Fields
        private ObservableCollection<Message> _messages = new ObservableCollection<Message>();
        #endregion

        #region Properties
        public ObservableCollection<Message> Messages
        {
            get { return _messages; }
        }
        #endregion

        public void AddMessage(Message msg)
        {
            _messages.Add(msg);
        }
    }
}
