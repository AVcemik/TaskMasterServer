using CsvHelper.Configuration;
using System.Globalization;

namespace TaskMasterServer.Service.Csv
{
    using CsvHelper;
    using System.Text;
    using TaskMasterServer.Data;

    internal interface ICsvString
    {
        //public static string CsvWriteString(Data data)
        //{
        //    CsvConfiguration config = new CsvConfiguration(CultureInfo.InvariantCulture);
        //    config.Encoding = Encoding.Unicode;

        //    string result = "";
        //    using StringWriter writer2 = new StringWriter();
        //    using (CsvWriter csvWriter2 = new CsvWriter(writer2, config))
        //    {
        //        csvWriter2.WriteRecords(data.GetUsers());
        //    }
        //    result += writer2.ToString();
        //    result += "^\n";
        //    using StringWriter writer = new StringWriter();
        //    using (CsvWriter csvWriter = new CsvWriter(writer, config))
        //    {
        //        csvWriter.WriteRecords(data.GetTasks());
        //    }
        //    result += writer.ToString();
        //    writer.Close();
        //    return result;     
        //}
    }
}
