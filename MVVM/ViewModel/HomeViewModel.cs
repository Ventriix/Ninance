using Ninance_v2.Core;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ninance_v2.MVVM.ViewModel
{
    public class HomeViewModel : ObservableObject
    {

        private double _balance = 00.00;
        private ObservableCollection<CsvTransaction> _lastTransactions;

        public double Balance
        {
            get { return _balance; } 
            set { _balance = value; OnPropertyChanged(); }
        }

        public Visibility ShowLastTransaction
        {
            get { return App.TransactionHandler.ListTransactions().Count() >= 1 ? Visibility.Visible : Visibility.Hidden; }
        }

        public ObservableCollection<CsvTransaction> LastTransactions
        {
            get { return _lastTransactions; }
            set { _lastTransactions = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
            UpdateBalance();
            UpdateLastTransactions();
        }

        public void UpdateBalance()
        {
            Balance = App.TransactionHandler.GetBalance();
        }

        public void UpdateLastTransactions()
        {
            var transactions = App.TransactionHandler.ListTransactions();
            transactions.Reverse();
            
            if(transactions.Count > 500)
                transactions.RemoveRange(5, transactions.Count - 1);

            LastTransactions = new ObservableCollection<CsvTransaction>(transactions);
        }

    }
}
