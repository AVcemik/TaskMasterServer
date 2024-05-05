using TaskMasterServer.Data;

namespace TaskMasterServer.DataBase
{
    public partial class Department
    {
        public Department()
        {
            Tasks = new HashSet<Task>();
            Users = new HashSet<User>();
        }

        public int DepartmentId { get; set; }
        public string? DepartmentName { get; set; }

        public virtual ICollection<Task> Tasks { get; set; }
        public virtual ICollection<User> Users { get; set; }

        /// <summary>
        /// Конвертация данных отдела под пользовательское приложение
        /// </summary>
        /// <returns>Возвращает данные отдела</returns>
        internal DepartmentData ConvertToData()
        {
            return new DepartmentData(DepartmentId, DepartmentName);
        }
    }
}
