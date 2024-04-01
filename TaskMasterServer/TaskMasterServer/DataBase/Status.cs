using System;
using System.Collections.Generic;

namespace TaskMasterServer.DataBase;

public partial class Status
{
    public int StatusId { get; set; }

    public string? StatusType { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
