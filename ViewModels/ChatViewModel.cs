using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace ChatApp.ViewModels
{
    public class ChatViewModel : ObservableObject
    {
        #region Fields

        #endregion

        #region Properties
        public RelayCommand SendCommand { get; private set; }
        //TODO: Make AnotherRequest Send Message To specific Client And Keep BroadCasting Request
        //public SelectedClient{}
        #endregion



        public ChatViewModel()
        {
            initCommands();
        }

        private void initCommands()
        {
            SendCommand = new RelayCommand(SendMessage);
        }


        public void SendMessage()
        {

        }
    }
}
