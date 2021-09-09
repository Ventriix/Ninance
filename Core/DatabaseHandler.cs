using CsvHelper;
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

        private string CsvPath = TransactionHandler.CsvPath;

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
                        csvReader.ValidateHeader<V1CsvTransaction>();

                        foreach(V1CsvTransaction v1CsvTransaction in csvReader.GetRecords<V1CsvTransaction>())
                        {
                            CsvTransaction convertedCsvTransaction = new CsvTransaction(TransactionHandler.GetIdForNewTransaction(), v1CsvTransaction.amount, v1CsvTransaction.direction, v1CsvTransaction.usage);
                            csvWriter.WriteRecord(convertedCsvTransaction);
                        }
                    }
                    else if (version == DatabaseType.NinanceV2)
                    {
                        csvReader.ValidateHeader<CsvTransaction>();
                        csvWriter.WriteRecords(csvReader.GetRecords<CsvTransaction>());
                    }
                }

                csvWriter.Flush();
            }

            return true;
        }

        public void ExportToFile(string path)
        {
            File.Copy(CsvPath, path);
        }
    }
}
