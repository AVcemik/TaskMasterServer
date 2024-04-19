using TaskMasterServer.Data;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal interface ICreateDataUser
    {
        public string CreateTask(TaskData task);
        public string CreateUser(UserData user);
        public string CreateDepartment();
    }
}
