﻿using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para Registry.xaml
    /// </summary>
    public partial class Registry : Window
    {
        public Registry()
        {
            InitializeComponent();
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Login loginView = new Login();
            loginView.Show();
            Close();
        }
    }
}
