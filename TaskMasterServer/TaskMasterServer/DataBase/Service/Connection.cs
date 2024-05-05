namespace TaskMasterServer.DataBase.Service
{
    internal class Connection
    {
        private static readonly string _publicServer = "Server=176.123.160.24; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";
        private static readonly string _localServer = "Server=localhost; Port=6432; Database=TaskUser_db; User Id=cemik; Password=22464427ceM;";

        /// <summary>
        /// Возвращает строку подключения по PublicHost
        /// </summary>
        /// <returns>Возвращает строку подключения</returns>
        internal string GetPublicConnection()
        {
            return _publicServer;
        }
        /// <summary>
        /// Возвращает строку подключения по LocalHost
        /// </summary>
        /// <returns>Возвращает строку подключения</returns>
        internal string GetLocalConnection()
        {
            return _localServer;
        }
    }

}
