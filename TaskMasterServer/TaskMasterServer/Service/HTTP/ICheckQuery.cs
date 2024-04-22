using System.Net;

namespace TaskMasterServer.Service.HTTP
{
    internal interface ICheckQuery
    {
        bool CheckContext(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request == null) { Console.WriteLine("Request - null"); return false; }
            else if (request.ContentType == null) { Console.WriteLine("Request.ContentType - null"); return false; }
            else if (response == null) { Console.WriteLine("Response - null"); return false; }
            else return true;
        }
    }
}
