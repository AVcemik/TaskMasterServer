namespace TaskMasterServer.DataBase
{
    public partial class User
    {
        public int UserId { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Firstname { get; set; }
        public string? Lastname { get; set; }
        public DateTime? Brithday { get; set; }
        public string? Contactphone { get; set; }
        public int? DepartmentId { get; set; }
        public bool? Isresponsible { get; set; }
        public bool? Isadmin { get; set; }

        public virtual Department? Department { get; set; }
        public virtual Authorization? Authorization { get; set; }
    }
}
