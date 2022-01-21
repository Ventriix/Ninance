using Ninance_v2.Core;
using System;

namespace Ninance_v2.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand HomeViewCommand { get; set; }
        public RelayCommand TransactionsViewCommand { get; set; }
        public RelayCommand AboutViewCommand { get; set; }
        public RelayCommand AddTransactionViewCommand { get; set; }

        public HomeViewModel HomeVM { get; set; }
        public TransactionsViewModel TransactionsVM { get; set; }
        public AboutViewModel AboutVM { get; set; }
        public AddTransactionViewModel AddTransactionVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set { _currentView = value; OnPropertyChanged(); }
        }

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
            CurrentView = HomeVM;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeVM;
            });

            TransactionsViewCommand = new RelayCommand(o =>
            {
                CurrentView = TransactionsVM;
            });

            AboutViewCommand = new RelayCommand(o =>
            {
                CurrentView = AboutVM;
            });

            AddTransactionViewCommand = new RelayCommand(o =>
            {
                CurrentView = AddTransactionVM;
            });
        }

        public void SearchTransaction(string usage)
        {
            if (usage.Length > 0)
            {
                if (CurrentView != TransactionsVM)
                {
                    TransactionsViewCommand.Execute(null);

                    /*((MainWindow)App.Current.MainWindow).HomeRadioButton.IsChecked = false;
                    ((MainWindow)App.Current.MainWindow).TransactionsRadioButton.IsChecked = true;
                    ((MainWindow)App.Current.MainWindow).AddTransactionRadioButton.IsChecked = false;
                    ((MainWindow)App.Current.MainWindow).AboutRadioButton.IsChecked = false;*/
                }

                TransactionsVM.TransactionsView.Filter = new Predicate<object>(transaction => { return usage == null || (transaction as CsvTransaction).Usage.IndexOf(usage, StringComparison.OrdinalIgnoreCase) != -1; });
            }
            else
                TransactionsVM.TransactionsView.Filter = null;
        }
    }
}
