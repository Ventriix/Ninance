using System;
using System.Windows;

namespace Ninance_v2
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            AutoUpdaterDotNET.AutoUpdater.DownloadPath = Environment.CurrentDirectory + "/temp";
            AutoUpdaterDotNET.AutoUpdater.Start("https://github.com/Ventriix/Ninance/blob/main/version_manifest.xml");

            WPFUI.Theme.Manager.Switch(App.ConfigHandler.Settings.UsingDarkMode ? WPFUI.Theme.Style.Dark : WPFUI.Theme.Style.Light);
            WPFUI.Background.Manager.Apply(this);
        }

        private void RootNavigation_OnLoaded(object sender, RoutedEventArgs e)
        {
            RootNavigation.Navigate("dashboard");
        }
    }
}
