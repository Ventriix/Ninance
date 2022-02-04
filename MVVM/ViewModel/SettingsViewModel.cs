using System.Windows.Controls;
using System.Windows.Input;
using Microsoft.Win32;
using Ninance_v2.Core;
using WPFUI.Controls;

namespace Ninance_v2.MVVM.ViewModel
{
    public class SettingsViewModel : ObservableObject
    {

        public RelayCommand DeleteDatabaseButtonCommand { get; set; }
        public RelayCommand ImportDatabaseButtonCommand { get; set; }
        public RelayCommand ImportOldDatabaseButtonCommand { get; set; }
        public RelayCommand ExportDatabaseButtonCommand { get; set; }



        private int _themeSelectorIndex;
        public int ThemeSelectorIndex
        {
            get { return _themeSelectorIndex; }
            set { 
                _themeSelectorIndex = value; 
                OnPropertyChanged();

                App.ConfigHandler.Settings.UsingDarkMode = value == 0;

                WPFUI.Theme.Manager.Switch(value == 0 ? WPFUI.Theme.Style.Dark : WPFUI.Theme.Style.Light);
                WPFUI.Background.Manager.Apply(App.Current.MainWindow);
            }
        }

        public SettingsViewModel()
        {
            ThemeSelectorIndex = App.ConfigHandler.Settings.UsingDarkMode ? 0 : 1;

            ImportOldDatabaseButtonCommand = new RelayCommand(parameter => {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".csv";
                dialog.Filter = "CSV Files (*.csv)|*.csv";
                dialog.Title = "Import Old (V1) CSV Database";

                bool? result = dialog.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    string fileName = dialog.FileName;
                    App.DatabaseHandler.ImportFile(fileName, DatabaseType.NinanceV1);
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true); // Nothing is more permanent than a temporary solution
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("transactions", true);
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true);
                }
            }, parameter => { return true; });

            ImportDatabaseButtonCommand = new RelayCommand(parameter => {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".csv";
                dialog.Filter = "CSV Files (*.csv)|*.csv";
                dialog.Title = "Import CSV Database";

                bool? result = dialog.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    string fileName = dialog.FileName;
                    App.DatabaseHandler.ImportFile(fileName, DatabaseType.NinanceV2);
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true); // Nothing is more permanent than a temporary solution
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("transactions", true);
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true);
                }
            }, parameter => { return true; });

            ExportDatabaseButtonCommand = new RelayCommand(parameter => {
                SaveFileDialog dialog = new SaveFileDialog();
                dialog.Title = "Export Database as CSV";
                dialog.FileName = "Ninance V2 Database";
                dialog.DefaultExt = ".csv";
                dialog.Filter = "CSV Files (*.csv)|*.csv";

                bool? result = dialog.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    string fileName = dialog.FileName;
                    App.DatabaseHandler.ExportToFile(fileName);
                }
            }, parameter => { return true; });

            DeleteDatabaseButtonCommand = new RelayCommand(parameter => {
                MessageBox messageBox = new MessageBox();

                messageBox.LeftButtonName = "Yes, delete!";
                messageBox.RightButtonName = "No, go back!";

                messageBox.LeftButtonClick += (object sender, System.Windows.RoutedEventArgs e) => {
                    App.DatabaseHandler.ResetDatabase();
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true); // Nothing is more permanent than a temporary solution
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("transactions", true);
                    ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true);
                    (sender as MessageBox)?.Close();
                };

                messageBox.RightButtonClick += (object sender, System.Windows.RoutedEventArgs e) => {
                    (sender as MessageBox)?.Close();
                };

                messageBox.Show("Confirmation", "Are you sure that you want to delete all data in the database?\nThis will delete all transactions.");
            }, parameter => { return true; });
        }
    }
}
