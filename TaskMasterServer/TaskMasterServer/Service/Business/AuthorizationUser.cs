using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business.CRUD;

namespace TaskMasterServer.Service.Business
{
    internal static class AuthorizationUser
    {
        public static Data.Data Login(UserData currentUser)
        {
            if (DataBd.ReadData().Users.Any(u => u.Login == currentUser.Login && u.Password == currentUser.Password))
            {
                UserData tempUser = DataBd.ReadData().Users.Where(u => u.Login == currentUser.Login && u.Password == currentUser.Password).First();
                return ReadData.GetData(tempUser);
            }
            else
            {
                return new Data.Data();
            }
        }
    }
}
