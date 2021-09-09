using Ninance_v2.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninance_v2.MVVM.ViewModel
{
    class TransactionsViewModel : ObservableObject
    {
        public List<CsvTransaction> Transactions
        {
            get { return App.TransactionHandler.ListTransactions(); }
            set { Transactions = value; OnPropertyChanged(); }
        }
    }
}
