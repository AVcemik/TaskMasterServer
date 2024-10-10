namespace taskMasterClientTest.Data
{
    internal class DepartmentDatas
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DepartmentDatas() { }

        public DepartmentDatas(string? name)
        {
            Name = name;
        }
    }
}
