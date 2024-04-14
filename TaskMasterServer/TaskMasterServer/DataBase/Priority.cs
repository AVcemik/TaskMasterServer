namespace TaskMasterServer.DataBase
{
    public partial class Priority
    {
        public Priority()
        {
            Tasks = new HashSet<Task>();
        }

        public int PriorityId { get; set; }
        public string? PriorityType { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
