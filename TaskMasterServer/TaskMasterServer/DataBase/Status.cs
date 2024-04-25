﻿using TaskMasterServer.Data;

namespace TaskMasterServer.DataBase
{
    public partial class Status
    {
        public Status()
        {
            Tasks = new HashSet<Task>();
        }

        public int StatusId { get; set; }
        public string? StatusType { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }

        internal StatusData ConvertToData()
        {
            return new StatusData(StatusId, StatusType);
        }
    }
}
