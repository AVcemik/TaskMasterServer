namespace TaskMasterServer.Data
{
    /// <summary>
    /// Данные статуса
    /// </summary>
    internal class StatusData
    {
        public int Id { get; set; }
        public string? StatusType { get; set; }

        public StatusData() { }
        public StatusData(int id, string? statusType)
        {
            Id = id;
            StatusType = statusType;
        }
    }
}
