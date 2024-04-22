//using TaskMasterServer.Data;
//using TaskMasterServer.DataBase;
//using Task = TaskMasterServer.DataBase.Task;

//namespace TaskMasterServer.Service.Business.CRUD.OldWork
//{
    //internal class Crud : IReadDataUser, ICreateDataUser, IUpdateDataUser
    //{
        //private readonly UserData _user;
        //private TaskData _task;
        //private Data.Data _data;

        //public Crud()
        //{
        //    _data = new Data.Data();
        //    _task = new TaskData();
        //}
        //public string CreateTask(TaskData task)
        //{
        //    Task taskBD = new Task();
        //    taskBD.TaskName = task.Title;
        //    taskBD.Description = task.Description;
        //    taskBD.DateCreate = task.StartDate;
        //    taskBD.Deadline = task.DeadLine;
        //    taskBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == task.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
        //    //taskBD.StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == task.Status).FirstOrDefault()?.StatusId ?? 0;
        //    taskBD.StatusId = 1;
        //    taskBD.PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == task.Priority).FirstOrDefault()?.PriorityId ?? 0;

        //    if (taskBD.DepartmentId == 0) return "Неверно указаный департамент";
        //    if (taskBD.StatusId == 0) return "Неверно указаный статус";
        //    if (taskBD.PriorityId == 0) return "Неверно указаный Приоритет";

        //    using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
        //    {
        //        dbContext.Add(taskBD);
        //        dbContext.SaveChanges();
        //    }
        //    return "Задача успешно добавлена";


        //}
        //public string CreateUser(UserData user, bool isAdmin)
        //{
        //    User userBD = new User();
        //    userBD.Login = user.Login;
        //    userBD.Password = user.Password;
        //    userBD.Email = user.Email;
        //    userBD.Firstname = user.FirstName;
        //    userBD.Lastname = user.LastName;
        //    userBD.Brithday = user.Birthday;
        //    userBD.Contactphone = user.ContactPhone;
        //    userBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == user.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
        //    userBD.Isresponsible = false;
        //    userBD.Isadmin = isAdmin;

        //    if (userBD.DepartmentId == 0) return "Неверно указан департамент";

        //    using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
        //    {
        //        dbContext.Add(userBD);
        //        dbContext.SaveChanges();
        //    }
        //    return "Пользователь успешно добавлен";
        //}
        //public string CreateDepartment(DepartmentData department)
        //{
        //    Department departmentBD = new Department();
        //    departmentBD.DepartmentName = department.Name;

        //    using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
        //    {
        //        dbContext.Add(departmentBD);
        //        dbContext.SaveChanges();
        //    }
        //    return "Департамент успешно добавлен";
        //}
        //public Data.Data ReadData(UserData user)
        //{
        //    if (user.Department == "Администратор")
        //    {
        //        foreach (var item in DataBd.ReadUser()) 
        //        {
        //            _user.GetUserDataConvertUserBD(item);
        //            _data.AddUser(_user);
        //        }
        //        foreach (var item in DataBd.ReadTask())
        //        {
        //            _task.GetTaskDataConvertTaskBD(item);
        //            _data.AddTask(_task);
        //        }
        //        return _data;
        //    }
        //    else
        //    {
        //        _data.AddUser(user);
        //        List<Task> taskBD = DataBd.ReadTask();

        //        var temp = DataBd.ReadTask().Where(t => t.Department!.DepartmentName == user.Department).ToList();
        //        foreach (var item in temp)
        //        {
        //            _task = new TaskData();
        //            _task.GetTaskDataConvertTaskBD(item);
        //            _data.AddTask(_task);
        //        }
        //        return _data;
        //    }
        //}
        //public string UpdateTask(TaskData task)
        //{
        //    Task taskBD = new Task();
        //    taskBD.TaskName = task.Title;
        //    taskBD.Description = task.Description;
        //    taskBD.DateCreate = task.StartDate;
        //    taskBD.Deadline = task.DeadLine;
        //    taskBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == task.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
        //    taskBD.StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == task.Status).FirstOrDefault()?.StatusId ?? 0;
        //    taskBD.PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == task.Priority).FirstOrDefault()?.PriorityId ?? 0;

        //    if (taskBD.DepartmentId == 0) return "Неверно указаный департамент";
        //    if (taskBD.StatusId == 0) return "Неверно указаный статус";
        //    if (taskBD.PriorityId == 0) return "Неверно указаный Приоритет";

        //    using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
        //    {
        //        dbContext.Update(taskBD);
        //        dbContext.SaveChanges();
        //    }
        //    return "Задача успешно добавлена";
        //}
//    }
//}
