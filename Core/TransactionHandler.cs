using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using CsvHelper;

namespace Ninance_v2.Core
{

    public class CsvTransaction
    {
        public int Id { get; set; }
        public long Timestamp { get; set; }
        public double Amount { get; set; }
        public double Tax { get; set; }
        public bool PlusOrMinus { get; set; } // True is plus and False is minus
        public string Usage { get; set; }

        public CsvTransaction(int Id, long Timestamp, double Amount, double Tax, bool PlusOrMinus, string Usage)
        {
            this.Id = Id;
            this.Timestamp = Timestamp;
            this.Amount = Amount;
            this.Tax = Tax;
            this.PlusOrMinus = PlusOrMinus;
            this.Usage = Usage;
        }

        public CsvTransaction(int Id, double Amount, double Tax, bool PlusOrMinus, string Usage)
        {
            this.Id = Id;
            Timestamp = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            this.Amount = Amount;
            this.Tax = Tax;
            this.PlusOrMinus = PlusOrMinus;
            this.Usage = Usage;
        }
    }

    public class TransactionHandler
    {

        public static string CsvPath = Environment.CurrentDirectory + "/data/transactions.csv";

        public TransactionHandler()
        {
            /* Create /data directory if it doesn't exist */
            if (!Directory.Exists(Environment.CurrentDirectory + "/data"))
                Directory.CreateDirectory(Environment.CurrentDirectory + "/data");

            /* Create .csv file if it doesn't exist */
            if (!File.Exists(CsvPath))
            {
                using (var stream = File.Open(CsvPath, FileMode.Create))
                using (var writer = new StreamWriter(stream))
                using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
                {
                    /* Write header and flush */
                    csv.WriteHeader<CsvTransaction>();
                    csv.NextRecord();
                    csv.Flush();

                    Console.WriteLine("Created data/transactions.csv");
                }
            }
        }

        public void AddTransaction(double amount, double tax, bool plusOrMinus, string usage)
        {
            int id = GetIdForNewTransaction();

            using (var stream = File.Open(CsvPath, FileMode.Append))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                CsvTransaction csvTransaction = new CsvTransaction(id, amount, tax, plusOrMinus, usage);

                /* Write new transaction record and flush */
                csv.WriteRecord(csvTransaction);
                csv.NextRecord();
                csv.Flush();
            }
        }

        public static int GetIdForNewTransaction()
        {
            return File.ReadLines(CsvPath).Count();
        }

        public void RemoveTransaction(int id)
        {
            List<CsvTransaction> allTransactions = ListTransactions();
            allTransactions.Remove(allTransactions.Find(transaction => transaction.Id == id));

            File.Delete(CsvPath);

            using (var stream = File.Open(CsvPath, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                /* Write header, records and flush */
                csv.WriteHeader<CsvTransaction>();
                csv.NextRecord();
                csv.WriteRecords(allTransactions);
                csv.Flush();
            }
        }

        public List<CsvTransaction> FindTransactionsByUsage(string usage)
        {
            List<CsvTransaction> transactions = new List<CsvTransaction>();

            foreach(CsvTransaction csvTransaction in ListTransactions())
                if (csvTransaction.Usage.Contains(usage))
                    transactions.Add(csvTransaction);

            return transactions;
        }

        public List<CsvTransaction> ListTransactions()
        {
            using (var reader = new StreamReader(CsvPath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                return csv.GetRecords<CsvTransaction>().ToList();
        }

        public double GetBalance()
        {
            double currentBalance = 0.0;

            foreach (CsvTransaction transaction in ListTransactions())
                if(transaction.PlusOrMinus)
                    currentBalance += transaction.Amount;
                else
                    currentBalance -= transaction.Amount;

            return currentBalance;
        }
    }
}
