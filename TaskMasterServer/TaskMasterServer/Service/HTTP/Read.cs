using TaskMasterServer.Data;
using TaskMasterServer.DataBase;

namespace TaskMasterServer.Service.HTTP
{
    internal class Read
    {
        public static List<TaskData> ReadTasks(UserData user)
        {
            List<TaskData> taskDatas = new List<TaskData>();
            User userBD = DataBd.ReadUser().Where(u => u.Login == user.Login).ToList()[0];
            if (userBD.Isadmin == true)
            {
                foreach (var item in DataBd.ReadTask())
                {
                    TaskData taskdata = new TaskData();
                    taskdata.GetTaskDataConvertTaskBD(item);
                    taskDatas.Add(taskdata);
                }
                return taskDatas;
            }
            else
            {
                List<DataBase.Task> userTaskBd = DataBd.ReadTask().Where(t => t.DepartmentId == int.Parse(userBD.DepartmentId.ToString()!)).ToList();
                foreach (var item in userTaskBd)
                {
                    TaskData userTaskData = new TaskData();
                    userTaskData.GetTaskDataConvertTaskBD(item);
                    taskDatas.Add(userTaskData);
                }
                return taskDatas;
            }
        }
        //public UserData ReadUser() { }
    }
}
