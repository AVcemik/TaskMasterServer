namespace TaskMasterServer.Data
{
    /// <summary>
    /// Данные пользователя
    /// </summary>
    internal class UserData
    {
        public int Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? Birthday { get; set; }
        public string? ContactPhone { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public string? Email { get; set; }
        public string? Department { get; set; }
        public bool? IsResponsible { get; set; } = false;
        public bool? IsAdmin { get; set; } = false;

        public UserData() { }
        public UserData(int id, string? firstName, string? lastName, DateTime? birthDay, string? contactPhone, string? login, string? password, string? email, string? department, bool? isResponsible, bool? isAdmin)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthDay;
            ContactPhone = contactPhone;
            Login = login;
            Password = password;
            Email = email;
            Department = department;
            IsResponsible = isResponsible;
            IsAdmin = isAdmin;
        }
    }
}
