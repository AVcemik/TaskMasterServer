using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace TaskMasterServer.Service.Csv
{
    using CsvHelper;
    using TaskMasterServer.Data;

    internal interface ICsvString
    {
        public static string CsvReadString(List<TaskUser> taskUser)
        {
            using StringWriter writer = new StringWriter();
            using (CsvWriter csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csvWriter.WriteRecords(taskUser);

            }
            return writer.ToString();
                
        }
    }
}
