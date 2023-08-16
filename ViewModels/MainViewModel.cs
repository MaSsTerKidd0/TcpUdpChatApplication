using ChatApp.Models;
using ChatApp.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        #region Fields
        private Dictionary<string, Client> clientTypes = new Dictionary<string, Client>()
        {
            { "Tcp" , new TcpClient() },
            { "Udp" , new UdpClient() }
        };
        private ComboBoxItem _selectedProtocol;
        #endregion

        #region Properties
        public string UserName { set { ClientInfo.Instance.UserName = value; } }
        public string Password { set { ClientInfo.Instance.Password = value; } }
        public ComboBoxItem SelectedProtocol
        {
            get { return _selectedProtocol; }
            set { _selectedProtocol = value; }
        }
        #endregion

        #region Commands
        public RelayCommand LoginCommand { get; private set; }
        #endregion

        #region Init
        public MainViewModel()
        {
            initCommands();
        }
        private void initCommands()
        {
            LoginCommand = new RelayCommand(ClientLogin);
        }

        #endregion

        #region CommandActions
        public void ClientLogin()
        {
            Client client = clientTypes[SelectedProtocol.Content.ToString()];

            ChatWindow chatWindow = new ChatWindow();
            Application.Current.MainWindow.Closed += (sender, e) =>
            {
                Application.Current.MainWindow = chatWindow;
                chatWindow.Show();
            };
            Application.Current.MainWindow.Close();
            client.StartClient();
        }


        #endregion
    }
}
