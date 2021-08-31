using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CsvHelper;

namespace Ninance_v2.Core
{

    public class CsvTransaction
    {
        public int id { get; set; }
        public float timestamp { get; set; }
        public double amount { get; set; }
        public bool plusOrMinus { get; set; }
        public string usage { get; set; }
    }

    public class TransactionHandler
    {

        private string csvPath = Environment.CurrentDirectory + "/data/transactions.csv";

        public TransactionHandler()
        {
            /* Create /data directory if it doesn't exist */
            if (!Directory.Exists(Environment.CurrentDirectory + "/data"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "/data");

            /* Create .csv file if it doesn't exist */
            if (!File.Exists(csvPath))
            {
                using (var stream = File.Open(csvPath, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    /* Write header and flush */
                    csv.WriteHeader<CsvTransaction>();
                    csv.Flush();
                }
            }
        }

        public void AddTransaction(double amount, bool plusOrMinus, string usage)
        {

        }
        
        public bool RemoveTransaction(int id)
        {
            return false;
        }

        public CsvTransaction[] FindTransactionsByUsage(string usage)
        {
            return null;
        }

        public List<CsvTransaction> ListTransactions(int max)
        {
            return null;
        }

    }
}
