using System.Text.Json;
using TaskMasterServer.Data;

namespace TaskMasterServer.Service.JSON
{
    internal class JsonReadData
    {
        public static UserData ReadUser(string requestBody)
        {
            UserData userData = new UserData();
            userData = JsonSerializer.Deserialize<UserData>(requestBody)!;
            return userData;
        }
        public static TaskData ReadTask(string requestBody)
        {
            TaskData taskData = new TaskData();
            taskData = JsonSerializer.Deserialize<TaskData>(requestBody)!;
            return taskData;
        }
    }
}
