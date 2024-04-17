using System.Net;
using TaskMasterServer.Data;

namespace TaskMasterServer.Service.HTTP
{
    internal class Server : IRequestInfo, ICheckQuery
    {
        private HttpListener _server = new HttpListener();
        private HttpListenerContext? _context;
        private HttpListenerRequest? _request;
        private HttpListenerResponse? _response;
        private string? _requestBody;
        private string? _contentType;
        private Data.Data _currentResponseInRequestData = new Data.Data();
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
                SetRequestBody(_request);
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
        public HttpListenerResponse? GetResponse()
        {
            return _response;
        }
        public string? GetContentType()
        {
            return _contentType;
        }
        public string GetRequestBody()
        {
            return _requestBody!;
        }
        private void SetRequestBody(HttpListenerRequest request)
        {
            string? requestBody = null;
            using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                requestBody = reader.ReadToEnd();
            }
            _requestBody = requestBody;
        }
        public void Send(string csvData, HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(csvData);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
