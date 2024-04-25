using System.Text.Json;
using TaskMasterServer.Data;

namespace TaskMasterServer.Service.JSON
{
    internal class JsonReadData
    {
        public static UserData ReadUser(string requestBody)
        {
            UserData userData = JsonSerializer.Deserialize<UserData>(requestBody)!;
            return userData;
        }
        //public static (UserData, bool) ReadUserAndIsAdmin(string requestBody)
        //{
        //    UserData userData = new UserData();

        //    bool checkSplit = false;
        //    foreach (char item in requestBody.ToCharArray())
        //    {
        //        if (item == '^') checkSplit = true;
        //    }

        //    if (checkSplit)
        //    {
        //        userData = JsonSerializer.Deserialize<UserData>(requestBody.Split('^')[0]);
        //        bool isAdmin = false;
        //        if (requestBody.Split('^')[1].ToLower() == "true") isAdmin = true;
        //        return (userData, isAdmin);
        //    }
        //    else return (userData, false);

        //}
        public static DepartmentData ReadDepartment(string requestBody)
        {
            DepartmentData department = new DepartmentData();
            department = JsonSerializer.Deserialize<DepartmentData>(requestBody)!;
            return department;
        }
        public static TaskData ReadTask(string requestBody)
        {
            TaskData taskData = new TaskData();
            taskData = JsonSerializer.Deserialize<TaskData>(requestBody)!;
            return taskData;
        }
    }
}
