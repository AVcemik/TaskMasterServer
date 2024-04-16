using System.Net;
using TaskMasterServer.Service.Csv;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.HTTP
{
    internal class Server : IRequestInfo, ICheckQuery, IResponseSend
    {
        private HttpListener _server = new HttpListener();
        private HttpListenerRequest _request;
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
            while (true)
            {
                Console.WriteLine($"Ожидание запроса №{_count}");
                HttpListenerContext context = _server.GetContext();
                _count++;

                if (((ICheckQuery)this).CheckContext(context) == false) continue;

                _request = context.Request;
                HttpListenerResponse response = context.Response; //???

                ((IRequestInfo)this).GetRequestInfo(_request);
                

                switch (_request.ContentType.Split(';').ToList()[0])
                {
                    case "application/auth": _data =  Logical.Authorization(JsonReadData.ReadUser(Logical.GetRequestBody(_request)));
                        break;
                }

                string csvData = ICsvString.CsvReadString(_data);
                ((IResponseSend)this).Send(csvData, response);

                Console.WriteLine($"Запрос от {_request.UserHostAddress} обработан");
            
            }
        }
    }
}
