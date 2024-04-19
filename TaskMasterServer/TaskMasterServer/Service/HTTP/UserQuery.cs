using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business.CRUD;

namespace TaskMasterServer.Service.HTTP
{
    internal static class UserQuery
    {
        public static Data.Data Authorization(UserData user)
        {
            bool isAuthorization = DataBd.ReadUser().Any(u => u.Login == user.Login && u.Password == user.Password);

            if (isAuthorization)
            {
                user.GetUserDataConvertUserBD(DataBd.ReadUser().Where(u => u.Login == user.Login && u.Password == user.Password).ToList().FirstOrDefault()!);
                Crud crud = new Crud();
                return crud.ReadData(user);
            }
            else
            {
                return new Data.Data();
            }
        }
        public static string TaskAdd(TaskData task)
        {
            Crud crud = new Crud();
            crud.CreateTask(task);
            return "Задача добавлена";
        }
    }
}
