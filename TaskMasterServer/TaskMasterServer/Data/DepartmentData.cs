namespace TaskMasterServer.Data
{
    internal class DepartmentData
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DepartmentData() { }

        public DepartmentData(int id, string? name)
        {
            Id = id;
            Name = name;
        }
    }
}
