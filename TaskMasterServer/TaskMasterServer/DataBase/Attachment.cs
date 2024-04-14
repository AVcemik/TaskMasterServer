namespace TaskMasterServer.DataBase
{
    public partial class Attachment
    {
        public Attachment()
        {
            Tasks = new HashSet<Task>();
        }

        public int AttachmentId { get; set; }
        public string? AttachmentPath { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
    }
}
