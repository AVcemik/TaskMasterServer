using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class DeleteData
    {
        public static string DeleteTask(TaskData deleteTask)
        {
            string result = "Неизвестная ошибка";

            if (DataBd.ReadTask()?.Any(t => t.TaskId == deleteTask.Id && t.TaskName == deleteTask.Title) ?? false)
            {
                Task taskDb = new()
                {
                    TaskId = deleteTask.Id,
                    TaskName = deleteTask.Title
                };
                using (TaskUser_dbContext dbContext = new())
                {
                    dbContext.Remove(taskDb);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Задача успешно удалена";
                };
            }
            else return "Id задачи не совподает с Названием задачи или один из параметров не указан";

            return result;
        }
        public static string DeleteUser(UserData deleteUser)
        {
            string result = "Неизвестная ошибка";

            if (DataBd.ReadUser()?.Any(u => u.UserId == deleteUser.Id && u.Login == deleteUser.Login) ?? false)
            {
                User userDb = new()
                {
                    UserId = deleteUser.Id,
                    Login = deleteUser.Login
                };
                using (TaskUser_dbContext dbContext = new())
                {
                    dbContext.Remove(userDb);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Пользователь успешно удален";
                };
            }
            else return "Id пользователя не совподает с логином или одно из полей не заполнено";

            return result;
        }
    }
}
