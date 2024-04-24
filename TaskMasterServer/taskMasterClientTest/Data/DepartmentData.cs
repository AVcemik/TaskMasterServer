namespace taskMasterClientTest.Data
{
    internal class DepartmentData
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DepartmentData() { }
        public DepartmentData(string name)
        {
            Name = name;
        }
    }
}
