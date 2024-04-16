using System.Net;

namespace TaskMasterServer.Service.HTTP
{
    public interface IRequestInfo
    {
        void GetRequestInfo(HttpListenerRequest request)
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Request.RawUrl: " + request.RawUrl);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Request Headers: " + request.Headers);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Request.Url: " + request.Url);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("UserHostAdress" + request.UserHostAddress);
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("Request ContentType: " + request.ContentType);
            Console.WriteLine("----------------------------------------------------------");

        }
    }
}
