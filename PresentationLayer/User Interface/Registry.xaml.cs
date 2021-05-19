using BusinessLayer.BusinessEntities;
using PresentationLayer.Helpers;
using System.Collections.Generic;
using System.Windows;
using Utils.CustomExceptions;

namespace PresentationLayer.User_Interface
{
    public partial class Registry : Window
    {
        private User _user = new User();
        private List<State> _states = new List<State>();
        public Registry()
        {
            InitializeComponent();
            this.DataContext = _user;
        }

        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {
            NavigateToLoginView();
        }

        private void SendButtonClicked(object sender, RoutedEventArgs e)
        {
            State selectedState = (State)ComboBoxState.SelectedItem;
            _user.Password = PasswordBoxPassword.Password;
            _user.State = selectedState;            

            try
            {
                _user.Register();
                NotificationWindow.ShowNotificationWindow("Usuario registrado",
                    "Su cuenta se ha registrado exitosamente. Ahora podrá iniciar sesión");
                NavigateToLoginView();
            }
            catch(NetworkRequestException networkRequestException)
            {
                NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
            }
        }

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            try
            {
                LoadStates();
            }catch(NetworkRequestException networkRequestException)
            {
                NotificationWindow.ShowErrorWindow("Error", networkRequestException.Message);
                NavigateToLoginView();
            }
            
        }

        private void LoadStates()
        {
            State state = new State();
            _states = state.GetStates();
            _states.ForEach(retrievedState =>
            {
                ComboBoxState.Items.Add(retrievedState);
            });
        }

        private void NavigateToLoginView()
        {
            Login loginView = new Login();
            loginView.Show();
            Close();
        }
    }
}
