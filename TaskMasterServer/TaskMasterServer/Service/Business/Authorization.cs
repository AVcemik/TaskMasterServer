using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business.CRUD;

namespace TaskMasterServer.Service.Business
{
    internal static class Authorization
    {
        public static Data.Data Login(UserData user)
        {
            bool isAuthorization = DataBd.ReadUser().Any(u => u.Login == user.Login && u.Password == user.Password);

            if (isAuthorization)
            {
                user.GetUserDataConvertUserBD(DataBd.ReadUser().Where(u => u.Login == user.Login && u.Password == user.Password).ToList().FirstOrDefault()!);
                return ReadData.ReadUserTasks(user);
            }
            else
            {
                return new Data.Data();
            }
        }
    }
}
