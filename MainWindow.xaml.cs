﻿using ChatApp.Views;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ChatApp
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            LoginSignUpPage signUpPage = new LoginSignUpPage();
            signUpPage.Show();
            InitializeComponent();
            itemListBox.Items.Add("Contact Example");
            itemListBox.Items.Add("Contact Example");
            itemListBox.Items.Add("Contact Example");
            itemListBox.Items.Add("Contact Example");
            itemListBox.Items.Add("Contact Example");
            itemListBox.Items.Add("Contact Example");
            itemListBox.Items.Add("Contact Example");
            itemListBox.Items.Add("Contact Example");
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
