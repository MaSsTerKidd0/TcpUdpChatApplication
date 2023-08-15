using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace ChatApp.Views
{
    public partial class LoginSignUpPage : Window
    {
        public LoginSignUpPage()
        {
            InitializeComponent();
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
