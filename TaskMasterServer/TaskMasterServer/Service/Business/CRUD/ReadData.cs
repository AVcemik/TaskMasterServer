using TaskMasterServer.Data;
using TaskMasterServer.DataBase;

namespace TaskMasterServer.Service.Business.CRUD
{
    /// <summary>
    /// Чтение данных из TempData сервера
    /// </summary>
    internal static class ReadData
    {
        /// <summary>
        /// Возвращает набор данных необходимый текущему пользователю
        /// </summary>
        /// <param name="currentUser">Экземпляр текущего пользователя</param>
        /// <returns>Возвращает данные для текущего пользователя классом Data</returns>
        public static Data.Data GetData(UserData currentUser)
        {
            Data.Data currentData = new();
            Data.Data data = DataBd.ReadData();

            List<TaskData> tasks = [];
            List<DepartmentData> departments = [];
            List<PriorityData> priorities = [];
            List<StatusData> statuses = [];

            currentData.Statuses = data.Statuses;
            currentData.Priorities = data.Priorities;
            currentData.Departments = data.Departments;

            if (currentUser.IsAdmin == true)
            {
                currentData.Tasks = data.Tasks;
                currentData.Users = data.Users;
                currentData.Users.Add(currentUser);
            }
            else
            {
                currentData.Tasks = data.Tasks.Where(t => t.Department == currentUser.Department).ToList();
                currentData.Users.Add(currentUser);
            }

            return currentData;
        }
    }
}
