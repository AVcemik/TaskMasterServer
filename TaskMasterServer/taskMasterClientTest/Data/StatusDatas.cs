namespace taskMasterClientTest.Data
{
    internal class StatusDatas
    {
        public int Id { get; set; }
        public string? StatusType { get; set; }

        public StatusDatas() { }

        public StatusDatas(string? statusType)
        {
            StatusType = statusType;
        }
    }
}
