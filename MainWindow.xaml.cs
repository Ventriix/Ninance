using System.Windows;

namespace Ninance_v2
{
    public partial class MainWindow : Window
    {

        public MainWindow()
        {
            InitializeComponent();

            WPFUI.Theme.Manager.Switch(WPFUI.Theme.Manager.GetSystemTheme());
            WPFUI.Background.Manager.Apply(this);
        }

        private void RootNavigation_OnLoaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Navigate("dashboard");
        }
    }
}
