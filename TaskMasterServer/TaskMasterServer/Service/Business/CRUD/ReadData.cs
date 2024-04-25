using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class ReadData
    {
        public static Data.Data GetData(UserData currentUser)
        {
            Data.Data currentData = new Data.Data();
            Data.Data data = DataBd.ReadData();
            List<TaskData> tasks = new List<TaskData>();
            List<Department> departments = new List<Department>();
            List<PriorityData> priorities = new List<PriorityData>();
            List<StatusData> statuses = new List<StatusData>();

            currentData.Users.Add(currentUser);
            currentData.Tasks = data.Tasks.Where(t => t.Department == currentUser.Department).ToList();
            currentData.Statuses = data.Statuses;
            currentData.Priorities = data.Priorities;
            currentData.Departments = data.Departments;


            return currentData;
        }
    }
}
