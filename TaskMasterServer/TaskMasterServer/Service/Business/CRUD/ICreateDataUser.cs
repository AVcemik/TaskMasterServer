using TaskMasterServer.Data;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal interface ICreateDataUser
    {
        public void CreateTask(TaskData task);
        public void CreateUser(UserData user);
        public void CreateDepartment();
    }
}
