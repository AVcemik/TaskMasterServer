namespace TaskMasterServer.Data
{
    /// <summary>
    /// Данные задачи
    /// </summary>
    internal class TaskData
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? DeadLine { get; set; }
        public string? Department { get; set; }
        public string? Status { get; set; }
        public string? Priority { get; set; }

        public TaskData() { }
        public TaskData(int id, string? title, string? description, DateTime? startDate, DateTime? deadLine, string department, string? status, string? priority)
        {
            Id = id;
            Title = title;
            Description = description;
            StartDate = startDate;
            DeadLine = deadLine;
            Department = department;
            Status = status;
            Priority = priority;
        }
    }
}
