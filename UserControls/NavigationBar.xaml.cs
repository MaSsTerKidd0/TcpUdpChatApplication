using System;
using System.Windows;
using System.Windows.Controls;

namespace ChatApp.UserControls
{
    /// <summary>
    /// Interaction logic for NavigationBar.xaml
    /// </summary>
    public partial class NavigationBar : UserControl
    {
        public event EventHandler MinimizeClicked;
        public event EventHandler MaximizeClicked;
        public event EventHandler ExitClicked;
        public NavigationBar()
        {
            InitializeComponent();
        }
        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            MinimizeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnMaximize_Click(object sender, RoutedEventArgs e)
        {
            MaximizeClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            ExitClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
