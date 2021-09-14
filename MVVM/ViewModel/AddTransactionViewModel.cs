using Ninance_v2.Core;

namespace Ninance_v2.MVVM.ViewModel
{
    class AddTransactionViewModel : ObservableObject
    {

        public RelayCommand AddTransactionCommand { get; set; }

        private string _usageText;
        public string UsageText
        {
            get { return _usageText; }
            set { _usageText = value; OnPropertyChanged(); }
        }

        private string _amountText;
        public string AmountText
        {
            get { return _amountText; }
            set { _amountText = value; OnPropertyChanged(); }
        }

        public AddTransactionViewModel()
        {
            AddTransactionCommand = new RelayCommand(o => {
                double parsedAmount = 00.00;

                if (UsageText == null || AmountText == null)
                    return;

                if (!AmountText.StartsWith("+") && !AmountText.StartsWith("-"))
                    return;

                if (!double.TryParse(AmountText.Substring(1), out parsedAmount))
                    return;

                App.TransactionHandler.AddTransaction(parsedAmount, AmountText.StartsWith("+"), UsageText);
                UsageText = "";
                AmountText = "";

                MainWindow mainWindow = new MainWindow();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = mainWindow;
                mainWindow.Show();
            }, o => { return true; });
        }
    }
}
