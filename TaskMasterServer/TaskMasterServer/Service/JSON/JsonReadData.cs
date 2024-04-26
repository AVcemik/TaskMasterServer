using System.Text.Json;
using TaskMasterServer.Data;

namespace TaskMasterServer.Service.JSON
{
    internal class JsonReadData
    {
        public static Data.Data ReadData(string requestBody)
        {
            Data.Data data = JsonSerializer.Deserialize<Data.Data>(requestBody)!;
            return data;
        }
        public static UserData ReadUser(string requestBody)
        {
            UserData userData = JsonSerializer.Deserialize<UserData>(requestBody)!;
            return userData;
        }
        public static TaskData ReadTask(string requestBody)
        {
            TaskData taskData = JsonSerializer.Deserialize<TaskData>(requestBody)!;
            return taskData;
        }
        public static DepartmentData ReadDepartment(string requestBody)
        {
            DepartmentData department = JsonSerializer.Deserialize<DepartmentData>(requestBody)!;
            return department;
        }
        public static PriorityData ReadPrioritet(string requestBody)
        {
            PriorityData taskData = JsonSerializer.Deserialize<PriorityData>(requestBody)!;
            return taskData;
        }
        public static StatusData ReadStatus(string requestBody)
        {
            StatusData statusData = JsonSerializer.Deserialize<StatusData>(requestBody)!;
            return statusData;
        }
    }
}
