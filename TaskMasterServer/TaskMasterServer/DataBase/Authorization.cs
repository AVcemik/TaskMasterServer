namespace TaskMasterServer.DataBase
{
    public partial class Authorization
    {
        public int UserId { get; set; }
        public string? Token { get; set; }
        public bool? Isauthorization { get; set; }

        public virtual User User { get; set; } = null!;
    }
}
