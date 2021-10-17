using CsvHelper;
using System;
using System.Globalization;
using System.IO;

namespace Ninance_v2.Core
{

    public enum DatabaseType
    {
        NinanceV1,
        NinanceV2,
        Unknown
    }

    public class V1CsvTransaction
    {
        public long timestamp { get; set; }
        public bool direction { get; set; }
        public double amount { get; set; }
        public string usage { get; set; }
    }

    public class DatabaseHandler
    {

        private readonly string CsvPath = TransactionHandler.CsvPath;

        public void ResetDatabase()
        {
            File.Delete(CsvPath);
            App.TransactionHandler = new TransactionHandler();
            App.DatabaseHandler = new DatabaseHandler();

            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = mainWindow;
            mainWindow.Show();
        }

        public bool ImportFile(string path, DatabaseType version)
        {
            File.Delete(CsvPath);

            using (var stream = File.Open(CsvPath, FileMode.Create))
            using (var writer = new StreamWriter(stream))
            using (var csvWriter = new CsvWriter(writer, CultureInfo.InvariantCulture))
            {
                csvWriter.WriteHeader<CsvTransaction>();
                csvWriter.NextRecord();

                if (version == DatabaseType.Unknown)
                    return false;

                using (var reader = new StreamReader(path))
                using (var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    if (version == DatabaseType.NinanceV1)
                    {
                        int id = 1;

                        foreach(V1CsvTransaction v1CsvTransaction in csvReader.GetRecords<V1CsvTransaction>())
                        {
                            CsvTransaction convertedCsvTransaction = new CsvTransaction(id, v1CsvTransaction.timestamp, v1CsvTransaction.amount, 0.00, v1CsvTransaction.direction, v1CsvTransaction.usage);
                            csvWriter.WriteRecord(convertedCsvTransaction);
                            csvWriter.NextRecord();

                            id++;
                        }
                    }
                    else if (version == DatabaseType.NinanceV2)
                        csvWriter.WriteRecords(csvReader.GetRecords<CsvTransaction>());
                }

                csvWriter.Flush();
            }

            MainWindow mainWindow = new MainWindow();
            App.Current.MainWindow.Close();
            App.Current.MainWindow = mainWindow;
            mainWindow.Show();

            return true;
        }

        public void ExportToFile(string path)
        {
            File.Copy(CsvPath, path);
        }
    }
}
