using BusinessLayer.BusinessEntities;
using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        private User _user = new User();
        public Login()
        {
            InitializeComponent();
            this.DataContext = _user;
        }

        private void CreateAccountButtonClicked(object sender, RoutedEventArgs e)
        {
            Registry registry = new Registry();
            registry.Show();
            Close();
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            _user.Password = PasswordBoxPassword.Password;                                    
            AccountActivation accountActivation = new AccountActivation();
            accountActivation.Show();
            Close();
        }

        private void ForgotPasswordButtonClicked(object sender, RoutedEventArgs e)
        {
            PasswordRecovery passwordRecovery = new PasswordRecovery();
            passwordRecovery.Show();
            Close();
        }
    }
}
