namespace TaskMasterServer.DataBase;

public partial class Priority
{
    public int PriorityId { get; set; }

    public string? PriorityType { get; set; }

    public virtual ICollection<Task> Tasks { get; set; } = new List<Task>();
}
