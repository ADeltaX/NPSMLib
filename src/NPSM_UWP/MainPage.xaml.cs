using NPSMLib;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace NPSM_UWP
{
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NowPlayingSessionManager npsm = new NowPlayingSessionManager();
            numTxt.Text = npsm.Count.ToString();
        }
    }
}
