using TaskMasterServer.Data;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal interface IUpdateDataUser
    {
        public string UpdateTask(TaskData task);
    }
}
