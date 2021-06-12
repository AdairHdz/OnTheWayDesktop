using System.Windows;

namespace PresentationLayer.Helpers
{
    public static class NotificationWindow
    {
        public static void ShowNotificationWindow(string title, string body)
        {
            MessageBox.Show(body, title,
                MessageBoxButton.OK, MessageBoxImage.Information);
        }

        public static void ShowErrorWindow(string title, string body)
        {
            MessageBox.Show(body, title,
                MessageBoxButton.OK, MessageBoxImage.Error);
        }
        
    }
}
