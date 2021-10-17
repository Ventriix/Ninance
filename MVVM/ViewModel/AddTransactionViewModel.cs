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

        private string _vatText;
        public string VatText
        {
            get { return _vatText; }
            set { _vatText = value; OnPropertyChanged(); }
        }

        private bool _amountIncludesVatBool = true;
        public bool AmountIncludesVatBool
        {
            get { return _amountIncludesVatBool; }
            set { _amountIncludesVatBool = value; OnPropertyChanged(); }
        }

        public AddTransactionViewModel()
        {
            AddTransactionCommand = new RelayCommand(o => {
                double parsedAmount = 00.00;
                double parsedVat = 0.00;
                bool direction = AmountText.StartsWith("+");

                if (UsageText == null || AmountText == null)
                    return;

                if (!AmountText.StartsWith("+") && !AmountText.StartsWith("-"))
                    return;

                if (!double.TryParse(AmountText.Substring(1), out parsedAmount))
                    return;

                if (!double.TryParse(VatText.Replace("%", ""), out parsedVat))
                    return;

                App.TransactionHandler.AddTransaction(AmountIncludesVatBool ? parsedAmount : (direction ? parsedAmount - (parsedAmount * (parsedVat / 100)) : parsedAmount + (parsedAmount * (parsedVat / 100))), AmountIncludesVatBool ? (direction ? parsedAmount - (parsedAmount * (parsedVat / 100)) : parsedAmount - (parsedAmount * (parsedVat / 100))) : parsedAmount,parsedVat, direction, UsageText);
                UsageText = "";
                AmountText = "";
                VatText = "";

                MainWindow mainWindow = new MainWindow();
                App.Current.MainWindow.Close();
                App.Current.MainWindow = mainWindow;
                mainWindow.Show();
            }, o => { return true; });
        }
    }
}
