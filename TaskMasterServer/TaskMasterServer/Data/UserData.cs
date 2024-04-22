namespace TaskMasterServer.Data
{
    internal class UserData
    {
        public int? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ContactPhone { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public bool? IsResponsible { get; set; }
        public UserData()
        {
            Id = 0;
            FirstName = null;
            LastName = null;
            Birthday = null;
            ContactPhone = null;
            Login = null;
            Password = null;
            Department = null;
            IsResponsible = false;
        }

        public UserData(int id, string? firstName, string? lastName, DateTime? birthDay, string? contactPhone, string? login, string? password, string? department, bool? isResponsible)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthDay;
            ContactPhone = contactPhone;
            Login = login;
            Password = password;
            Department = department;
            IsResponsible = isResponsible;
        }
        public void GetUserDataConvertUserBD(DataBase.User userBd)
        {
            Id = userBd.UserId;
            FirstName = userBd.Firstname;
            LastName = userBd.Lastname;
            Birthday = userBd.Brithday;
            ContactPhone = userBd.Contactphone;
            Login = userBd.Login;
            Password = userBd.Password;
            Department = userBd.Department!.DepartmentName;
            IsResponsible = userBd.Isresponsible;
        }
    }
}
