using System.Windows;

namespace Ninance_v2
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            WPFUI.Theme.Manager.Switch(App.ConfigHandler.Settings.UsingDarkMode ? WPFUI.Theme.Style.Dark : WPFUI.Theme.Style.Light);
            WPFUI.Background.Manager.Apply(App.Current.MainWindow);
        }

        private void RootNavigation_OnLoaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Navigate("dashboard");
        }
    }
}
