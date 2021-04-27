using BusinessLayer.BusinessEntities;
using System.Windows;

namespace PresentationLayer.User_Interface
{
    public partial class Registry : Window
    {
        private User _user = new User();
        public Registry()
        {
            InitializeComponent();
            this.DataContext = _user;
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            Login loginView = new Login();
            loginView.Show();
            Close();
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            _user.Password = PasswordBoxPassword.Password;       
        }
    }
}
