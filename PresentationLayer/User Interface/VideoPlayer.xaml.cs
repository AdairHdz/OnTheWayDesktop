using System.Windows;

namespace PresentationLayer.User_Interface
{
    /// <summary>
    /// Lógica de interacción para VideoPlayer.xaml
    /// </summary>
    public partial class VideoPlayer : Window
    {
        private bool _isPaused;        
        public VideoPlayer()
        {
            InitializeComponent();            
            Video.Play();                
            _isPaused = false;                                    
        }

        private void MediaButtonClicked(object sender, RoutedEventArgs e)
        {            
            if (_isPaused)
            {
                Video.Play();
                ButtonIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Pause;
                _isPaused = false;
            }
            else if (!_isPaused && Video.CanPause)
            {
                Video.Pause();
                _isPaused = true;
                ButtonIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            }
        }

        private void VolumeSliderValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Video.Volume = VideoVolumeSlider.Value;            
        }

        private void VideoEnded(object sender, RoutedEventArgs e)
        {
            ButtonIcon.Kind = MaterialDesignThemes.Wpf.PackIconKind.Play;
            Video.Stop();           
            _isPaused = true;
        }
    }
}
