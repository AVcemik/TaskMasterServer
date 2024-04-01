using System;
using System.Collections.Generic;

namespace TaskMasterServer.DataBase;

public partial class Department
{
    public int DepartmentId { get; set; }

    public string? DepartmentName { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
