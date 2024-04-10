namespace TaskMasterServer.Data
{
    internal class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        //public DateTime? Bithday { get; set; }
        //public string? ContactPhone { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Department { get; set; }

        public User(int id, string? name, string? surname, string? login, string? password, string? department)
        {
            Id = id;
            Name = name;
            Surname = surname;
            Login = login;
            Password = password;
            Department = department;
        }
        //public bool IsResponsible { get; set; }

    }
}
