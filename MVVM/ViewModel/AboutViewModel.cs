using Microsoft.Win32;
using Ninance_v2.Core;
using System.Windows.Input;

namespace Ninance_v2.MVVM.ViewModel
{
    class AboutViewModel : ObservableObject
    {
        public ICommand ImportV1DatabaseCommand { get; set; }
        public ICommand ImportV2DatabaseCommand { get; set; }
        public ICommand ExportDatabaseCommand { get; set; }
        public ICommand ResetDatabaseCommand { get; set; }

        public AboutViewModel()
        {
            ImportV1DatabaseCommand = new RelayCommand(parameter => {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".csv";
                dialog.Filter = "CSV Files (*.csv)|*.csv";
                dialog.Title = "Import Old CSV Database";

                bool? result = dialog.ShowDialog();

                if(result.HasValue && result.Value)
                {
                    string fileName = dialog.FileName;
                    App.DatabaseHandler.ImportFile(fileName, DatabaseType.NinanceV1);
                }
            }, parameter => { return true; });

            ImportV2DatabaseCommand = new RelayCommand(parameter => {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.DefaultExt = ".csv";
                dialog.Filter = "CSV Files (*.csv)|*.csv";
                dialog.Title = "Import CSV Database";

                bool? result = dialog.ShowDialog();

                if (result.HasValue && result.Value)
                {
                    string fileName = dialog.FileName;
                    App.DatabaseHandler.ImportFile(fileName, DatabaseType.NinanceV2);
                    
                }
            }, parameter => { return true; });

            ExportDatabaseCommand = new RelayCommand(parameter => {
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

            ResetDatabaseCommand = new RelayCommand(parameter =>
            {
                App.DatabaseHandler.ResetDatabase();
            });
        }
    }
}
