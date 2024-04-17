using TaskMasterServer.Data;
using TaskMasterServer.DataBase;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal class Crud : IReadDataUser
    {
        private readonly UserData _user;
        private readonly TaskData _task;
        private Data.Data _data;

        public Crud()
        {
            _data = new Data.Data();
            _task = new TaskData();
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
