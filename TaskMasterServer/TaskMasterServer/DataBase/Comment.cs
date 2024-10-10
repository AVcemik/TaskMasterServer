namespace TaskMasterServer.DataBase
{
    public partial class Comment
    {
        public int CimmentId { get; set; }
        public string? Comment1 { get; set; }
        public int? TaskId { get; set; }

        public virtual Task? Task { get; set; }
    }
}
