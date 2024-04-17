namespace TaskMasterServer.DataBase.Service
{
    public enum ConnectionBD
    {
        Server,
        Yandex,
        Loft,
        CyberPunk,
        Home
    }
    public static class Connection
    {
        private static readonly string _server = "Server=176.123.160.24; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _yandex = "Server=192.168.000.000; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _loft = "Server=192.168.000.000; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _cyberPunk = "Server=192.168.10.59; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _home = "Server=192.168.40.3; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";

        public static string StringConnection(ConnectionBD stringConndection)
        {
            switch (stringConndection)
            {
                case ConnectionBD.Server: return _server;
                case ConnectionBD.Yandex: return _yandex;
                case ConnectionBD.Loft: return _loft;
                case (ConnectionBD.CyberPunk): return _cyberPunk;
                case ConnectionBD.Home: return _home;
                default: return _server;
            }
        }
    }
}
