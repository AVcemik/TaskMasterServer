namespace TaskMasterServer.Data
{
    internal class Data
    {
        public List<UserData> Users { get; set; } = new List<UserData>();
        public List<TaskData> Tasks { get; set; } = new List<TaskData>();
        public List<DepartmentData> Departments { get; set; } = new List<DepartmentData>();
        public List<PriorityData> Priorities { get; set; } = new List<PriorityData>();
        public List<StatusData> Statuses { get; set; } = new List<StatusData>();

        public Data()
        {

        }
    }
}
