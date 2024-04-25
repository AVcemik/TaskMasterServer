using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business.CRUD;

namespace TaskMasterServer.Service.Business
{
    internal static class Authorization
    {
        public static Data.Data Login(UserData currentUser)
        {
            UserData tempUser = (UserData)DataBd.ReadData().Users.Where(u => u.Login == currentUser.Login && u.Password == currentUser.Password);

            if (tempUser != null)
            {
               return ReadData.GetData(tempUser);
            }
            else
            {
                return new Data.Data();
            }
        }
    }
}
