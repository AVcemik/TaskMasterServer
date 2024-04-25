using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class CreateData
    {
        public static string CreateTask(TaskData task)
        {
            Task taskBD = new Task();
            taskBD.TaskName = task.Title;
            taskBD.Description = task.Description;
            taskBD.DateCreate = task.StartDate;
            taskBD.Deadline = task.DeadLine;
            taskBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == task.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            //taskBD.StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == task.Status).FirstOrDefault()?.StatusId ?? 0;
            taskBD.StatusId = 1;
            taskBD.PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == task.Priority).FirstOrDefault()?.PriorityId ?? 0;

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
        public static string CreateUser(UserData user)
        {
            User userBD = new User();
            userBD.Login = user.Login;
            userBD.Password = user.Password;
            userBD.Email = user.Email;
            userBD.Firstname = user.FirstName;
            userBD.Lastname = user.LastName;
            userBD.Brithday = user.Birthday;
            userBD.Contactphone = user.ContactPhone;
            userBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == user.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            userBD.Isresponsible = false;
            userBD.Isadmin = user.IsAdmin;

            if (userBD.DepartmentId == 0) return "Неверно указан департамент";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(userBD);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Пользователь успешно добавлен";
        }
        public static string CreateDepartment(DepartmentData department)
        {
            Department departmentBD = new Department();
            departmentBD.DepartmentName = department.Name;

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Add(departmentBD);
                dbContext.SaveChanges();
            }
            DataBd.UpdateTempBD();
            return "Департамент успешно добавлен";
        }
    }
}
