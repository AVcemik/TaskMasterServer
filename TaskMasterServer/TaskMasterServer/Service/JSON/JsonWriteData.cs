using System.Text.Encodings.Web;
using System.Text.Json;
using TaskMasterServer.Data;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TaskMasterServer.Service.JSON
{
    /// <summary>
    /// Запись в JSON строку
    /// </summary>
    internal class JsonWriteData
    {
        public static string WriteData(Data.Data data)
        {
            string result = JsonSerializer.Serialize(data, GetJsonOptionDefault());
            return result;
        }
        public static string WriteUser(List<UserData> users)
        {
            string result = JsonSerializer.Serialize(users, GetJsonOptionDefault());
            return result;
        }
        public static string WriteTask(List<TaskData> tasks)
        {
            string result = JsonSerializer.Serialize(tasks, GetJsonOptionDefault());
            return result;
        }
        public static string WriteDepartment(List<DepartmentData> departments)
        {
            string result = JsonSerializer.Serialize(departments, GetJsonOptionDefault());
            return result;
        }
        public static string WritePrioritet(List<PriorityData> prioritets)
        {
            string result = JsonSerializer.Serialize(prioritets, GetJsonOptionDefault());
            return result;
        }
        public static string WriteStatus(List<StatusData> statuses)
        {
            string result = JsonSerializer.Serialize(statuses, GetJsonOptionDefault());
            return result;
        }

        /// <summary>
        /// Опции Json для корректной записи
        /// </summary>
        /// <returns>Возвращает опции Json</returns>
        private static JsonSerializerOptions GetJsonOptionDefault()
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                WriteIndented = true,
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            return options;
        }
    }
}
