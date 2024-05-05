using System.Net;

namespace TaskMasterServer.Service.HTTP
{
    internal interface IResponseSend
    {
        public void Send(string message, HttpListenerResponse response);
    }
}
