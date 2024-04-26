namespace taskMasterClientTest.Data
{
    internal class Data
    {
        public List<UserDatas> Users { get; set; } = new List<UserDatas>();
        public List<TaskDatas> Tasks { get; set; } = new List<TaskDatas>();
        public List<DepartmentDatas> Departments { get; set; } = new List<DepartmentDatas>();
        public List<PriorityDatas> Priorities { get; set; } = new List<PriorityDatas>();
        public List<StatusDatas> Statuses { get; set; } = new List<StatusDatas>();
    }
}
