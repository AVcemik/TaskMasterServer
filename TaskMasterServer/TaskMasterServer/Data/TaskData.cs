using TaskMasterServer.DataBase;

namespace TaskMasterServer.Data
{
    public class TaskData
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }

        public TaskData() { }
        public TaskData(int id, string? title, string? description, DateTime? startDate, DateTime? deadLine, string? status, string? priority)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            DeadLine = deadLine;
            Status = status;
            Priority = priority;
        }
        public void GetTaskDataConvertTaskBD(DataBase.Task taskBd)
        {
            Id = taskBd.TaskId;
            Title = taskBd.TaskName;
            Description = taskBd.Description;
            StartDate = taskBd.DateCreate;
            DeadLine = taskBd.Deadline;
            Status = taskBd.Status!.StatusType;
            Priority = taskBd.Priority!.PriorityType;
        }
    }
}
