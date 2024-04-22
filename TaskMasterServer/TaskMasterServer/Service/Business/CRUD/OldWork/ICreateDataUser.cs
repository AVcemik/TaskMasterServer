using TaskMasterServer.Data;

namespace TaskMasterServer.Service.Business.CRUD.OldWork
{
    internal interface ICreateDataUser
    {
        public string CreateTask(TaskData task);
        public string CreateUser(UserData user, bool isAdmin);
        public string CreateDepartment(DepartmentData deparment);
    }
}
