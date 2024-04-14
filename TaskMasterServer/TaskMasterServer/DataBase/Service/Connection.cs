namespace TaskMasterServer.DataBase.Service
{
    public enum ConnectionBD
    {
        Server,
        Yandex,
        Loft,
        Home
    }
    public static class Connection
    {
        private static readonly string _server = "Server=176.123.160.24; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _yandex = "Server=176.123.160.24; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _loft = "Server=176.123.160.24; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _home = "Server=176.123.160.24; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";

        public static string StringConnection(ConnectionBD stringConndection)
        {
            switch (stringConndection)
            {
                case ConnectionBD.Server: return _server;
                case ConnectionBD.Yandex: return _yandex;
                case ConnectionBD.Loft: return _loft;
                case ConnectionBD.Home: return _home;
                default: return _server;
            }
        }
    }
}
