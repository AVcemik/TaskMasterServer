using TaskMasterServer.Data;
using TaskMasterServer.DataBase;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal class Crud : IReadDataUser, ICreateDataUser
    {
        private readonly UserData _user;
        private readonly TaskData _task;
        private Data.Data _data;

        public Crud()
        {
            _data = new Data.Data();
            _task = new TaskData();
        }
        public void CreateTask(TaskData task)
        {
            DataBase.Task taskBD = new DataBase.Task();
            taskBD.TaskName = task.Title;
            taskBD.Description = task.Description;
            taskBD.DateCreate = task.StartDate;
            taskBD.Deadline = task.DeadLine;
            taskBD.DepartmentId = DataBd.ReadDepartment().Where(d => d.DepartmentName == task.Department).FirstOrDefault().DepartmentId;
            taskBD.StatusId = DataBd.ReadStatuses().Where(s => s.StatusType == task.Status).FirstOrDefault().StatusId;
            taskBD.PriorityId = DataBd.ReadPriority().Where(p => p.PriorityType == task.Priority).FirstOrDefault().PriorityId;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(taskBD);
            }

        }
        public void CreateUser(UserData user)
        {
            throw new NotImplementedException();
        }
        public void CreateDepartment()
        {
            throw new NotImplementedException();
        }
        public Data.Data ReadData(UserData user)
        {
            if (user.Department == "Администратор")
            {
                foreach (var item in DataBd.ReadUser()) 
                {
                    _user.GetUserDataConvertUserBD(item);
                    _data.AddUser(_user);
                }
                foreach (var item in DataBd.ReadTask())
                {
                    _task.GetTaskDataConvertTaskBD(item);
                    _data.AddTask(_task);
                }
                return _data;
            }
            else
            {
                _data.AddUser(user);
                foreach (var item in DataBd.ReadTask().Where(t => t.Department!.DepartmentName == user.Department).ToList())
                {
                    _task.GetTaskDataConvertTaskBD(item);
                    _data.AddTask(_task);
                }
                return _data;
            }
        }
    }
}
