using System.Net;
using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Csv;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.HTTP
{
    internal class Server : IRequestInfo, ICheckQuery, IResponseSend
    {
        private HttpListener _server = new HttpListener();
        private HttpListenerContext? _context;
        private HttpListenerRequest? _request;
        private HttpListenerResponse? _response;
        private string? _requestBody;
        private string? _contentType;
        private Data.Data _data = new Data.Data();
        private int _count = 1;
        public Server()
        {
            _server.Prefixes.Add($"http://*:{8080}/");
            _server.Prefixes.Add($"http://+:{8080}/");
        }

        public void Start()
        {
            _server.Start();
            Console.WriteLine("Сервер запущен");
        }

        public void QueryProcessing()
        {
            Console.WriteLine($"Ожидание запроса №{_count}");
            _context = _server.GetContext();
            _count++;

            if (((ICheckQuery)this).CheckContext(_context) == true)
            {
                _request = _context.Request;
                _response = _context.Response;
                _requestBody = Logical.GetRequestBody(_request);
                _contentType = _request.ContentType!.Split(';').FirstOrDefault();
            }
            else
            {
                _request = null;
                _response = null;
                _requestBody = null;
                _contentType = null;
                Console.WriteLine("Неверный запрос");
            } 
        }
        public HttpListenerRequest? GetRequest()
        {
            return _request;
        }
        public string? GetContentType()
        {
            return _contentType;
        }
    }
}
