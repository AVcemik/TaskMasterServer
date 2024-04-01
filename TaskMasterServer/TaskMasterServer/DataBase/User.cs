using System;
using System.Collections.Generic;

namespace TaskMasterServer.DataBase;

public partial class User
{
    public int UserId { get; set; }

    public string? Login { get; set; }

    public string? Password { get; set; }

    public string? Email { get; set; }

    public string? UserName { get; set; }

    public int? DepartmentId { get; set; }

    public bool? Isresponsible { get; set; }

    public virtual Department? Department { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
