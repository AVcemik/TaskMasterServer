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

    public virtual Authorization? Authorization { get; set; }

    public virtual Department? Department { get; set; }
}
