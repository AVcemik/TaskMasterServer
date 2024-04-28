using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class CreateData
    {
        public static string CreateTask(TaskData newTask)
        {
            string result = "Неизвестная ошибка";
            Task taskDB = new()
            {
                TaskName = newTask.Title,
                Description = newTask.Description,
                DateCreate = newTask.StartDate,
                Deadline = newTask.DeadLine,
                DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == newTask.Department)?.FirstOrDefault()?.DepartmentId ?? 0,
                StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == newTask.Status).FirstOrDefault()?.StatusId ?? 0,
                PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == newTask.Priority).FirstOrDefault()?.PriorityId ?? 0
            };

            if (taskDB.TaskName == null || taskDB.TaskName.Trim() == "") return "Заголовок задачи не указан";
            if (taskDB.Description == null || taskDB.Description.Trim() == "") return "Описание задачи не указанно";
            if (taskDB.DateCreate == null) return "Неизвестная ошибка с датой создания";
            if (taskDB.Deadline == null) return "Срок выполнения задачи не указан";
            if (taskDB.DepartmentId == 0) return "Неверно указан департамент";
            if (taskDB.StatusId == 0) return "Неверно указан статус";
            if (taskDB.PriorityId == 0) return "Неверно указан приоритет";

            using (TaskUser_dbContext dbContext = new())
            {
                dbContext.Add(taskDB);
                dbContext.SaveChanges();
                result = "Задача успешно добавлена";
            }
            DataBd.UpdateTempBD();
            return result;
        }
        public static string CreateUser(UserData newUser)
        {
            string result = "Неизвестная ошибка";
            User userDB = new()
            {
                Login = newUser.Login,
                Password = newUser.Password,
                Email = newUser.Email,
                Firstname = newUser.FirstName,
                Lastname = newUser.LastName,
                Brithday = newUser.Birthday,
                Contactphone = newUser.ContactPhone,
                DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == newUser.Department)?.FirstOrDefault()?.DepartmentId ?? 0,
                Isresponsible = false,
                Isadmin = newUser.IsAdmin
            };

            if (userDB.Login == null || userDB.Login.Trim() == "") return "Логин пользователя не указан";
            if (userDB.Password == null || userDB.Password.Trim() == "") return "Пароль пользователя не указан";
            if (userDB.Email == null || userDB.Email.Trim() == "") return "E-mail пользователя не указан";
            if (userDB.Firstname == null || userDB.Firstname.Trim() == "") return "Имя пользоваателя не указано";
            if (userDB.Lastname == null || userDB.Lastname.Trim() == "") return "Фамилия пользоваателя не указана";
            if (userDB.Brithday == null) return "Дата рождения пользователя не указана";
            if (userDB.Contactphone == null || userDB.Contactphone.Trim() == "") return "Контактный телефон пользователя не указан";
            if (userDB.DepartmentId == 0) return "Неверно указан департамент";

            using (TaskUser_dbContext dbContext = new())
            {
                dbContext.Add(userDB);
                dbContext.SaveChanges();
                result = "Пользователь успешно добавлен";
            }
            DataBd.UpdateTempBD();
            return result;
        }
        public static string CreateDepartment(DepartmentData newDepartment)
        {
            string result = "Неизвестная ошибка";
            Department departmentDB = new()
            {
                DepartmentName = newDepartment.Name
            };

            if (departmentDB.DepartmentName == null || departmentDB.DepartmentName.Trim() == "") return "Не указано название отдела";

            using (TaskUser_dbContext dbContext = new())
            {
                dbContext.Add(departmentDB);
                dbContext.SaveChanges();
                result = "Деапартамент успешно добавлен";
            }
            DataBd.UpdateTempBD();
            return result;
        }
        public static string CreatePrioritet(PriorityData newPrioritet)
        {
            string result = "Неизвестная ошибка";
            Priority priorityDB = new()
            {
                PriorityType = newPrioritet.PriorityType
            };

            if (priorityDB.PriorityType == null || priorityDB.PriorityType.Trim() == "") return "Не указано название приоритета";

            using (TaskUser_dbContext dbContext = new())
            {
                dbContext.Add(priorityDB);
                dbContext.SaveChanges();
                result = "Приоритет успешно добавлен";
            }
            DataBd.UpdateTempBD();
            return result;
        }
        public static string CreateStatus(StatusData newStatus)
        {
            string result = "Неизвестная ошибка";
            Status statusDB = new()
            {
                StatusType = newStatus.StatusType
            };

            if (statusDB.StatusType == null || statusDB.StatusType.Trim() == "") return "Не указано название статуса";

            using (TaskUser_dbContext dbContext = new())
            {
                dbContext.Add(statusDB);
                dbContext.SaveChanges();
                result = "Статус успешно добавлен";
            }
            DataBd.UpdateTempBD();
            return result;
        }
    }
}
