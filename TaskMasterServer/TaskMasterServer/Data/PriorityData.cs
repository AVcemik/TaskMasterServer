namespace TaskMasterServer.Data
{
    /// <summary>
    /// Данные приоритета
    /// </summary>
    internal class PriorityData
    {
        public int Id { get; set; }
        public string? PriorityType { get; set; }

        public PriorityData() { }
        public PriorityData(int id, string? priorityType)
        {
            Id = id;
            PriorityType = priorityType;
        }
    }
}
