using Ninance_v2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Ninance_v2.MVVM.ViewModel
{
    class HomeViewModel : ObservableObject
    {

        private string _displayedBalance = "00.00€";
        private Visibility _showLastTransaction = Visibility.Visible;

        public string DisplayedBalance
        {
            get { return _displayedBalance; } 
            set { _displayedBalance = value; OnPropertyChanged(); }
        }

        public Visibility ShowLastTransaction
        {
            get { return App.TransactionHandler.ListTransactions().Count() >= 1 ? Visibility.Visible : Visibility.Hidden; }
            set { _showLastTransaction = value; OnPropertyChanged(); }
        }

        public HomeViewModel()
        {
        }

        public void UpdateDisplayedBalance()
        {
            DisplayedBalance = App.TransactionHandler.GetBalance() + "€";
        }

    }
}
