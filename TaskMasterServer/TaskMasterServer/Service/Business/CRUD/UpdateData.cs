using System.Threading.Tasks;
using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class UpdateData
    {
        public static string UpdateTask(TaskData updateTask)
        {
            string result = "Неизвестная ошибка";
            Task taskDB = new Task();
            taskDB.TaskId = updateTask.Id;
            taskDB.TaskName = updateTask.Title;
            taskDB.Description = updateTask.Description;
            taskDB.DateCreate = DataBd.ReadTask()?.Where(t => t.TaskId == updateTask.Id)?.FirstOrDefault()?.DateCreate ?? null;
            taskDB.Deadline = updateTask.DeadLine;
            taskDB.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == updateTask.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            taskDB.StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == updateTask.Status).FirstOrDefault()?.StatusId ?? 0;
            taskDB.PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == updateTask.Priority).FirstOrDefault()?.PriorityId ?? 0;

            if (taskDB.StatusId == 0) return "Неверно указан Id задачи";
            if (taskDB.TaskName == null || taskDB.TaskName.Trim() == "") return "Заголовок задачи не указан";
            if (taskDB.Description == null || taskDB.Description.Trim() == "") return "Описание задачи не указанно";
            //if (taskBD.DateCreate == null) return "Неизвестная ошибка с датой создания";
            if (taskDB.Deadline == null) return "Срок выполнения задачи не указан";
            if (taskDB.DepartmentId == 0) return "Неверно указан департамент";
            if (taskDB.StatusId == 0) return "Неверно указан статус";
            if (taskDB.PriorityId == 0) return "Неверно указан приоритет";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                if (DataBd.ReadData().Tasks.Any(t => t.Id == taskDB.TaskId) == true)
                {
                    dbContext.Update(taskDB);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Задача успешно обновлена";
                }
                else result = "Задача не найдена";
            }
            return result;
        }
        public static string UpdateUser(UserData updateUser)
        {
            string result = "Неизвестная ошибка";
            User userDB = new User
            {
                UserId = updateUser.Id,
                Login = updateUser.Login,
                Password = updateUser.Password,
                Email = updateUser.Email,
                Firstname = updateUser.FirstName,
                Lastname = updateUser.LastName,
                Brithday = updateUser.Birthday,
                Contactphone = updateUser.ContactPhone,
                DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == updateUser.Department)?.FirstOrDefault()?.DepartmentId ?? 0,
                Isresponsible = false,
                Isadmin = updateUser.IsAdmin
            };

            if (userDB.UserId == 0) return "Не верно указан Id пользователя";
            if (userDB.Login == null || userDB.Login.Trim() == "") return "Логин пользователя не указан";
            if (userDB.Password == null || userDB.Password.Trim() == "") return "Пароль пользователя не указан";
            if (userDB.Email == null || userDB.Email.Trim() == "") return "E-mail пользователя не указан";
            if (userDB.Firstname == null || userDB.Firstname.Trim() == "") return "Имя пользоваателя не указано";
            if (userDB.Lastname == null || userDB.Lastname.Trim() == "") return "Фамилия пользоваателя не указана";
            if (userDB.Brithday == null) return "Дата рождения пользователя не указана";
            if (userDB.Contactphone == null || userDB.Contactphone.Trim() == "") return "Контактный телефон пользователя не указан";
            if (userDB.DepartmentId == 0) return "Неверно указан департамент";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                if (DataBd.ReadData().Users.Any(u => u.Id == userDB.UserId) == true)
                {
                    dbContext.Update(userDB);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Данные пользователя успешно обновлены";
                }
                else result = "Пользователь не найден";
            }
            return result;
        }
        public static string UpdateDepartment(DepartmentData updateDepartment)
        {
            string result = "Неизвестная ошибка";
            Department departmentDB = new()
            {
                DepartmentName = updateDepartment.Name
            };

            if (departmentDB.DepartmentId == 0) return "Не верно указан Id отдела";
            if (departmentDB.DepartmentName == null || departmentDB.DepartmentName.Trim() == "") return "Не указано название отдела";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                if (DataBd.ReadData().Departments.Any(d => d.Id == departmentDB.DepartmentId) == true)
                {
                    dbContext.Update(departmentDB);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Департамент успешно обновлен";
                }
                else result = "Департамент не найден";
            }
            return result;
        }
        public static string UpdatePrioritet(PriorityData updatePrioritet)
        {
            string result = "Неизвестная ошибка";
            Priority priorityDB = new()
            {
                PriorityType = updatePrioritet.PriorityType
            };

            if (priorityDB.PriorityId == 0) return "Не верно указан Id приоритета";
            if (priorityDB.PriorityType == null || priorityDB.PriorityType.Trim() == "") return "Не указано название приоритета";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                if (DataBd.ReadData().Priorities.Any(p => p.Id == priorityDB.PriorityId) == true)
                {
                    dbContext.Update(priorityDB);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Приоритет успешно добавлен";
                }
                else result = "Приоритет не найден";
            }
            return result;
        }
        public static string UpdateStatus(StatusData updateStatus)
        {
            string result = "Неизвестная ошибка";
            Status statusDB = new()
            {
                StatusType = updateStatus.StatusType
            };

            if (statusDB.StatusId == 0) return "Не верно указан Id статуса";
            if (statusDB.StatusType == null || statusDB.StatusType.Trim() == "") return "Не указано название статуса";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                if (DataBd.ReadData().Statuses.Any(d => d.Id == statusDB.StatusId) == true)
                {
                    dbContext.Update(statusDB);
                    dbContext.SaveChanges();
                    DataBd.UpdateTempBD();
                    result = "Статус успешно обновлен";
                }
            }
            return result;
        }
    }
}
