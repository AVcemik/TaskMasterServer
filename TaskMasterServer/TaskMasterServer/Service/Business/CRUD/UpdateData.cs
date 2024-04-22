using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using Task = TaskMasterServer.DataBase.Task;

namespace TaskMasterServer.Service.Business.CRUD
{
    internal static class UpdateData
    {
        public static string UpdateTask(TaskData task)
        {
            Task taskBD = new Task();
            taskBD.TaskName = task.Title;
            taskBD.Description = task.Description;
            taskBD.DateCreate = task.StartDate;
            taskBD.Deadline = task.DeadLine;
            taskBD.DepartmentId = DataBd.ReadDepartment()?.Where(d => d.DepartmentName == task.Department)?.FirstOrDefault()?.DepartmentId ?? 0;
            taskBD.StatusId = DataBd.ReadStatuses()?.Where(s => s.StatusType == task.Status).FirstOrDefault()?.StatusId ?? 0;
            taskBD.PriorityId = DataBd.ReadPriority()?.Where(p => p.PriorityType == task.Priority).FirstOrDefault()?.PriorityId ?? 0;

            if (taskBD.DepartmentId == 0) return "Неверно указаный департамент";
            if (taskBD.StatusId == 0) return "Неверно указаный статус";
            if (taskBD.PriorityId == 0) return "Неверно указаный Приоритет";

            using (TaskUser_dbContext dbContext = new TaskUser_dbContext())
            {
                dbContext.Update(taskBD);
                dbContext.SaveChanges();
            }
            return "Задача успешно добавлена";
        }
    }
}
