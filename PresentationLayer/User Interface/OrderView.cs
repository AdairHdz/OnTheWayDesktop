using System.Windows;


namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para OrderView.xaml
    /// </summary>
    public partial class OrderView : Window
    {
        public OrderView()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            ReportMisconduct report = new ReportMisconduct();
            report.Show();
            this.Close();
        }
        private void BackButtonClicked(object sender, RoutedEventArgs e)
        {

        }
    }
}
