using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class CreateData
    {
        public static string CreateTask(TaskData newTask)
        {
            Task taskBD = new Task();
            taskBD.TaskName = newTask.Title;
            taskBD.Description = newTask.Description;
            taskBD.DateCreate = newTask.StartDate;
            taskBD.Deadline = newTask.DeadLine;
            taskBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == newTask.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            taskBD.StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == newTask.Status).FirstOrDefault()?.StatusId ?? 0;
            taskBD.PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == newTask.Priority).FirstOrDefault()?.PriorityId ?? 0;

            if (taskBD.DepartmentId == 0) return "Неверно указаный департамент";
            if (taskBD.StatusId == 0) return "Неверно указаный статус";
            if (taskBD.PriorityId == 0) return "Неверно указаный Приоритет";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(taskBD);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Задача успешно добавлена";


        }
        public static string CreateUser(UserData newUser)
        {
            User userBD = new User();
            userBD.Login = newUser.Login;
            userBD.Password = newUser.Password;
            userBD.Email = newUser.Email;
            userBD.Firstname = newUser.FirstName;
            userBD.Lastname = newUser.LastName;
            userBD.Brithday = newUser.Birthday;
            userBD.Contactphone = newUser.ContactPhone;
            userBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == newUser.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            userBD.Isresponsible = false;
            userBD.Isadmin = newUser.IsAdmin;

            if (userBD.DepartmentId == 0) return "Неверно указан департамент";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(userBD);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Пользователь успешно добавлен";
        }
        public static string CreateDepartment(DepartmentData newDepartment)
        {
            Department departmentDB = new Department();
            departmentDB.DepartmentName = newDepartment.Name;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(departmentDB);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Департамент успешно добавлен";
        }
        public static string CreatePrioritet(PriorityData newPrioritet)
        {
            Priority priorityDB = new Priority();
            priorityDB.PriorityType = newPrioritet.PriorityType;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(priorityDB);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Статус успешно добавлен";
        }
        public static string CreateStatus(StatusData newStatus)
        {
            Status statusDB = new Status();
            statusDB.StatusType = newStatus.StatusType;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(statusDB);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Статус успешно добавлен";
        }
    }
}
