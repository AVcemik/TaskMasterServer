namespace TaskMasterServer.DataBase.Service
{
    public enum ConnectionDB
    {
        PublicDbConnection,
        LocalDbConnection
    }
    public static class Connection
    {
        private static readonly string _publicServer = "Server=176.123.160.24; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _localServer = "Server=localhost; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";

        public static string StringConnection(ConnectionDB stringConndection)
        {
            switch (stringConndection)
            {
                case ConnectionDB.PublicDbConnection: return _publicServer;
                case ConnectionDB.LocalDbConnection: return _localServer;
                default: return _localServer;
            }
        }
    }
}
