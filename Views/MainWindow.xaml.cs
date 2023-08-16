using ChatApp.ViewModels;
using System;
using System.Windows;
using System.Windows.Input;

namespace ChatApp.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            MainViewModel mainViewModel = new MainViewModel();
            DataContext = mainViewModel;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void UserControlNavbar_MinimizeClicked(object sender, EventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void UserControlNavbar_MaximizeClicked(object sender, EventArgs e)
        {
            if (WindowState == WindowState.Maximized)
            {
                WindowState = WindowState.Normal;
            }
            else
            {
                WindowState = WindowState.Maximized;
            }
        }

        private void UserControlNavbar_ExitClicked(object sender, EventArgs e)
        {
            Application.Current.Shutdown();
        }
    }


}
