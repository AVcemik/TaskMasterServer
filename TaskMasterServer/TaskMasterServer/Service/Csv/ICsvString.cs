using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;

namespace TaskMasterServer.Service.Csv
{
    using CsvHelper;
    using TaskMasterServer.Data;

    internal interface ICsvString
    {
        public static string CsvReadString(List<TaskUser> taskUser, List<Data.User> user)
        {
            string result = "";
            using StringWriter writer2 = new StringWriter();
            using (CsvWriter csvWriter2 = new CsvWriter(writer2, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csvWriter2.WriteRecords(user);
            }
            result += writer2.ToString();
            result += "^\n";
            using StringWriter writer = new StringWriter();
            using (CsvWriter csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
            {
                csvWriter.WriteRecords(taskUser);
            }
            result += writer.ToString();
            writer.Close();
            return result;
                
        }
    }
}
