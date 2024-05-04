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
        public static string DeleteDepartment(DepartmentData deleteDepartment)
        {
            string result = "Неизвестная ошибка";

            if (DataBd.ReadDepartment()?.Any(t => t.DepartmentId == deleteDepartment.Id && t.DepartmentName == deleteDepartment.Name) ?? false)
            {
                Department departmentDb = new()
                {
                    DepartmentId = deleteDepartment.Id,
                    DepartmentName = deleteDepartment.Name
                };
                using (TaskUser_dbContext dbContext = new())
                {
                    dbContext.Remove(departmentDb);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Департамент успешно удален";
                };
            }
            else return "Id департамента не совподает с Названием департамента или один из параметров не указан";

            return result;
        }
        public static string DeleteStatus(StatusData deleteStatus)
        {
            string result = "Неизвестная ошибка";

            if (DataBd.ReadStatuses()?.Any(t => t.StatusId == deleteStatus.Id && t.StatusType == deleteStatus.StatusType) ?? false)
            {
                Status statusDb = new()
                {
                    StatusId = deleteStatus.Id,
                    StatusType = deleteStatus.StatusType
                };
                using (TaskUser_dbContext dbContext = new())
                {
                    dbContext.Remove(statusDb);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Статус успешно удален";
                };
            }
            else return "Id статуса не совподает с Названием статуса или один из параметров не указан";

            return result;
        }

        public static string DeletePrioritet(PriorityData deletePrioritet)
        {
            string result = "Неизвестная ошибка";

            if (DataBd.ReadPriority()?.Any(t => t.PriorityId == deletePrioritet.Id && t.PriorityType == deletePrioritet.PriorityType) ?? false)
            {
                Priority priorityDb = new()
                {
                    PriorityId = deletePrioritet.Id,
                    PriorityType = deletePrioritet.PriorityType
                };
                using (TaskUser_dbContext dbContext = new())
                {
                    dbContext.Remove(priorityDb);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Приоритет успешно удален";
                };
            }
            else return "Id приоритета не совподает с Названием приоритета или один из параметров не указан";

            return result;
        }
    }
}
