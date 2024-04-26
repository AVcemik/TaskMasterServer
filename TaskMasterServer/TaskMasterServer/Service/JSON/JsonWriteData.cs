using System.Text.Json;

namespace TaskMasterServer.Service.JSON
{
    internal class JsonWriteData
    {
        internal static string WriteData(Data.Data data)
        {
            string? result = null;

            char delimitir = '^';
            result += JsonSerializer.Serialize(data.Users);
            result += delimitir;
            result += JsonSerializer.Serialize(data.Tasks);
            result += delimitir;
            result += JsonSerializer.Serialize(data.Departments);
            result += delimitir;
            result += JsonSerializer.Serialize(data.Priorities);
            result += delimitir;
            result += JsonSerializer.Serialize(data.Statuses);

            return result;
        }
    }
}
