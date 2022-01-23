using Ninance_v2.Core;
using System;

namespace Ninance_v2.MVVM.ViewModel
{
    public class MainViewModel : ObservableObject
    {
        public HomeViewModel HomeVM { get; set; }
        public TransactionsViewModel TransactionsVM { get; set; }
        public AboutViewModel AboutVM { get; set; }
        public AddTransactionViewModel AddTransactionVM { get; set; }
        public SettingsViewModel SettingsVM { get; set; }

        private string _transactionSearchBarText;
        public string TransactionSearchBarText
        {
            get { return _transactionSearchBarText; }
            set { _transactionSearchBarText = value; OnPropertyChanged(); SearchTransaction(value); }
        }

        public MainViewModel()
        {
            HomeVM = new HomeViewModel();
            TransactionsVM = new TransactionsViewModel();
            AboutVM = new AboutViewModel();
            AddTransactionVM = new AddTransactionViewModel();
            SettingsVM = new SettingsViewModel();
        }

        public void SearchTransaction(string usage)
        {
            if (usage.Length > 0)
            {
                TransactionsVM.TransactionsView.Filter = new Predicate<object>(transaction => { return usage == null || (transaction as CsvTransaction).Usage.IndexOf(usage, StringComparison.OrdinalIgnoreCase) != -1; });
            }
            else
                TransactionsVM.TransactionsView.Filter = null;
        }
    }
}
