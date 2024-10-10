namespace taskMasterClientTest.Data
{
    internal class PriorityDatas
    {
        public int Id { get; set; }
        public string? PriorityType { get; set; }

        public PriorityDatas() { }

        public PriorityDatas(string? priorityType)
        {
            PriorityType = priorityType;
        }
    }
}
