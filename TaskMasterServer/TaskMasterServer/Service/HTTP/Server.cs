using System.Net;
using TaskMasterServer.Service.Encryptions;

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
        private int _count = 1;
        private int _port = 8080;
        private bool _prefixServer = true;
        public Server(bool prefixServer, int port)
        {
            _port = port;
            _prefixServer = prefixServer;

            if (_prefixServer)
            {
                _server.Prefixes.Add($"http://*:{_port}/");
                _server.Prefixes.Add($"http://+:{_port}/");
            }
            else
            {
                _server.Prefixes.Add($"http://localhost:{_port}/");
            }
        }

        /// <summary>
        /// Запуск сервера
        /// </summary>
        public void Start()
        {
            _server.Start();
            Console.WriteLine("Сервер запущен");
        }

        /// <summary>
        /// Ожидание запроса
        /// </summary>
        /// <param name="isWhileContinue">Проверка запрос на отсутсвие null</param>
        public void QueryProcessing(out bool isWhileContinue)
        {
            Console.WriteLine($"\nОжидание запроса №{_count}");
            _context = _server.GetContext();
            _count++;

            if (CheckContext(_context) == true)
            {
                _request = _context.Request;
                _response = _context.Response;
                SetRequestBody(_request);
                _contentType = _request.ContentType!.Split(';').FirstOrDefault();
                GetRequestInfo();
                isWhileContinue = false;
            }
            else
            {
                Console.WriteLine("Некоректный запрос\n");
                isWhileContinue=true;
            } 
        }

        // Возвращает текущий объект запроса
        public HttpListenerRequest? GetRequest()
        {
            return _request;
        }

        // Возвращает текущий объект ответа
        public HttpListenerResponse? GetResponse()
        {
            return _response;
        }

        // Возвращает текущий Тип контента (Строку запроса для определения реакции сервера на запрос клиента)
        public string? GetContentType()
        {
            return _contentType;
        }

        // Возвращает тело переданное клиентом
        public string GetRequestBody()
        {
            return Encryption.DecryptString(_requestBody!);
        }

        // Устанавливаем новое тело для передачи клиенту
        private void SetRequestBody(HttpListenerRequest request)
        {
            string? requestBody = null;
            using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                requestBody = reader.ReadToEnd();
            }
            _requestBody = requestBody;
        }

        /// <summary>
        /// Ответ клиенту
        /// </summary>
        /// <param name="data">Тело в формате JSON</param>
        /// <param name="response">Представляет ответ клиенту</param>
        public void Send(string data, HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(data);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);
            output.Close();
        }

        /// <summary>
        /// Проверка контекста на nulls, во избежания exeption
        /// </summary>
        /// <param name="context">Принимаемый контекст</param>
        /// <returns>Bool значение сообщающее об успешной операции</returns>
        public bool CheckContext(HttpListenerContext context)
        {
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            if (request == null) { Console.WriteLine("Request - null"); return false; }
            else if (request.ContentType == null) { Console.WriteLine("Request.ContentType - null"); return false; }
            else if (response == null) { Console.WriteLine("Response - null"); return false; }
            else return true;
        }

        /// <summary>
        /// Вывод дополнительной текущей информации (Адрес клиента и запрос)
        /// </summary>
        public void GetRequestInfo()
        {
            Console.WriteLine("----------------------------------------------------------");
            Console.WriteLine("IP клиента: " + _request!.RemoteEndPoint.Address);
            Console.WriteLine("Запрос: " + _contentType);
            Console.WriteLine("----------------------------------------------------------");
        }
    }
}
