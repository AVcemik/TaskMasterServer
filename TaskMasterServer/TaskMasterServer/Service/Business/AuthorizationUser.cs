using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business.CRUD;

namespace TaskMasterServer.Service.Business
{
    /// <summary>
    /// Временный класс авторизации пользователей (Будет подвержен глобальным изменениям)
    /// </summary>
    internal static class AuthorizationUser
    {
        /// <summary>
        /// Авторизация (И возврат данных)
        /// </summary>
        /// <param name="currentUser">Текущий пользователь, пытающийся авторизоваться</param>
        /// <returns>Возвращаем данные</returns>
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
