using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
