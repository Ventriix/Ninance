using Ninance_v2.Core;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Data;
using System.Windows.Input;

namespace Ninance_v2.MVVM.ViewModel
{
    public class TransactionsViewModel : ObservableObject
    {

        private ObservableCollection<CsvTransaction> _transactions;

        public ObservableCollection<CsvTransaction> Transactions
        {
            get { return _transactions; }
            set { _transactions = value; OnPropertyChanged(); TransactionsView.Refresh(); }
        }

        public ICollectionView TransactionsView
        {
            get { return CollectionViewSource.GetDefaultView(Transactions); }
        }

        public RelayCommand DeleteTransaction { get; set; }

        public TransactionsViewModel()
        {
            DeleteTransaction = new RelayCommand(parameter => {
                App.TransactionHandler.RemoveTransaction((int)parameter);
                UpdateTransactions();
            }, parameter => { return parameter is int; });

            UpdateTransactions();
        }

        public void UpdateTransactions()
        {
            var transactions = App.TransactionHandler.ListTransactions();
            transactions.Reverse();

            Transactions = new ObservableCollection<CsvTransaction>(transactions);
        }
    }
}
