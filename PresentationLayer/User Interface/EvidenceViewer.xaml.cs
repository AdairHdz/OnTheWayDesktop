using System;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para EvidenceViewer.xaml
    /// </summary>
    public partial class EvidenceViewer : Window
    {
        private bool _isPaused = false;
        private string _linkOfEvidence = "";
        public EvidenceViewer(string linkOfEvidence)
        {
            InitializeComponent();
            _linkOfEvidence = linkOfEvidence;
        }



        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            string source = Flurl.Url.Combine("http://localhost:8080/", _linkOfEvidence);
            MediaPlayer.Source = new Uri(source);
            MediaPlayer.Play();            
        }

        private void MediaElementWasClicked(object sender, MouseButtonEventArgs e)
        {
            if (MediaPlayer.CanPause)
            {
                if(_isPaused)
                {
                    MediaPlayer.Play();
                    _isPaused = false;                    
                }
                else
                {
                    MediaPlayer.Pause();
                    _isPaused = true;
                }
            }
        }
    }
}
