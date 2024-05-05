using System.Net;

namespace TaskMasterServer.Service.HTTP
{
    internal interface ICheckQuery
    {
        public bool CheckContext(HttpListenerContext context);
    }
}
