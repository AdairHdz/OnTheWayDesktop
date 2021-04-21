
using System;
using System.ComponentModel;
using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para ReportMisconduct.xaml
    /// </summary>
    public partial class ReportMisconduct : Window
    {
        public ReportMisconduct()
        {
            InitializeComponent();
        }
        private void Window_Closing(object sender, CancelEventArgs e)
        {
            Environment.Exit(1);
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
