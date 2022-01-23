using Ninance_v2.Core;

namespace Ninance_v2.MVVM.ViewModel
{
    public class AddTransactionViewModel : ObservableObject
    {
        public RelayCommand AddTransactionCommand { get; set; }

        private string _usageText;
        public string UsageText
        {
            get { return _usageText; }
            set { _usageText = value; OnPropertyChanged(); }
        }

        private string _amount;
        public string Amount
        {
            get { return _amount; }
            set { _amount = value; OnPropertyChanged(); }
        }

        private int _selectedPlusMinusIndex;
        public int SelectedPlusMinusIndex
        {
            get { return _selectedPlusMinusIndex; }
            set { _selectedPlusMinusIndex = value; OnPropertyChanged(); }
        }

        public AddTransactionViewModel()
        {
            AddTransactionCommand = new RelayCommand(o => {
                if (UsageText == null)
                    return;

                if (Amount == "")
                    return;

                double parsedAmount;

                if (!double.TryParse(Amount.Replace(".", ","), out parsedAmount))
                    return;

                App.TransactionHandler.AddTransaction(parsedAmount, SelectedPlusMinusIndex == 0, UsageText);
                UsageText = "";
                Amount = "";

                ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true); // Nothing is more permanent than a temporary solution
                ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("transactions", true);
                ((MainWindow)App.Current.MainWindow).RootNavigation.Navigate("dashboard", true);
            }, o => { return true; });
        }
    }
}
