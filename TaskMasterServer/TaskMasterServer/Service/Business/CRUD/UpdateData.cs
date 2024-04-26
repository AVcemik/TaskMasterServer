using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class UpdateData
    {
        public static string UpdateTask(TaskData updateTask)
        {
            Task taskBD = new Task();
            taskBD.TaskName = updateTask.Title;
            taskBD.Description = updateTask.Description;
            taskBD.DateCreate = updateTask.StartDate;
            taskBD.Deadline = updateTask.DeadLine;
            taskBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == updateTask.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            taskBD.StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == updateTask.Status).FirstOrDefault()?.StatusId ?? 0;
            taskBD.PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == updateTask.Priority).FirstOrDefault()?.PriorityId ?? 0;

            if (taskBD.DepartmentId == 0) return "Неверно указаный департамент";
            if (taskBD.StatusId == 0) return "Неверно указаный статус";
            if (taskBD.PriorityId == 0) return "Неверно указаный Приоритет";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Update(taskBD);
                dbContext.SaveChanges();
            }
            return "Задача успешно изменена";
        }
        public static string UpdateUser(UserData updateUser)
        {
            User userBD = new User();
            userBD.Login = updateUser.Login;
            userBD.Password = updateUser.Password;
            userBD.Email = updateUser.Email;
            userBD.Firstname = updateUser.FirstName;
            userBD.Lastname = updateUser.LastName;
            userBD.Brithday = updateUser.Birthday;
            userBD.Contactphone = updateUser.ContactPhone;
            userBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == updateUser.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            userBD.Isresponsible = false;
            userBD.Isadmin = updateUser.IsAdmin;

            if (userBD.DepartmentId == 0) return "Неверно указан департамент";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Update(userBD);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Данные пользователя успешно изменены";
        }
        public static string UpdateDepartment(DepartmentData updateDepartment)
        {
            Department departmentDB = new Department();
            departmentDB.DepartmentName = updateDepartment.Name;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Update(departmentDB);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Департамент успешно обновлен";
        }
        public static string UpdatePrioritet(PriorityData updatePrioritet)
        {
            Priority priorityDB = new Priority();
            priorityDB.PriorityType = updatePrioritet.PriorityType;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(priorityDB);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Приоритет успешно обновлен";
        }
        public static string UpdateStatus(StatusData updateStatus)
        {
            Status statusDB = new Status();
            statusDB.StatusType = updateStatus.StatusType;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(statusDB);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Статус успешно обновлен";
        }
    }
}
