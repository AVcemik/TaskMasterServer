using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class ReadData
    {
        public static Data.Data ReadUserTasks(UserData user)
        {
            UserData users = new UserData();
            TaskData tasks = new TaskData();
            Data.Data data = new Data.Data();

            if (user.Department == "Администратор")
            {
                foreach (var item in DataBd.ReadUser())
                {
                    users.GetUserDataConvertUserBD(item);
                    data.AddUser(users);
                }
                foreach (var item in DataBd.ReadTask())
                {
                    tasks.GetTaskDataConvertTaskBD(item);
                    data.AddTask(tasks);
                }
                return data;
            }
            else
            {
                data.AddUser(user);
                List<Task> taskBD = DataBd.ReadTask();

                var temp = DataBd.ReadTask().Where(t => t.Department!.DepartmentName == user.Department).ToList();
                foreach (var item in temp)
                {
                    tasks = new TaskData();
                    tasks.GetTaskDataConvertTaskBD(item);
                    data.AddTask(tasks);
                }
                return data;
            }
        }
    }
}
