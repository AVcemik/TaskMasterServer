﻿namespace taskMasterClientTest.Data
{
    public class TaskData
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
        public TaskData(string? title, string? description, DateTime? startDate, DateTime? deadLine, string department, string? status, string? priority)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            DeadLine = deadLine;
            Department = department;
            Status = status;
            Priority = priority;
        }
        public TaskData(string? title, string? description, DateTime? startDate, DateTime? deadLine, string? status, string? priority)
        {
            Title = title;
            Description = description;
            StartDate = startDate;
            DeadLine = deadLine;
            Status = status;
            Priority = priority;
        }
    }
}
