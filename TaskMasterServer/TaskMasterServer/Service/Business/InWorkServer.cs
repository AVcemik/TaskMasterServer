using System.Net;
using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Csv;
using TaskMasterServer.Service.HTTP;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.Business
{
    internal class InWorkServer
    {
        private Server _server = new Server();
        private int _count = 1;
        User userDb = new User();
        public void Start()
        {
            _server.Start();
        }
        public void InWorkProcess()
        {
            while (true)
            {
                _server.QueryProcessing();
                _count++;



                switch (_server.GetContentType())
                {
                    case "application/auth":
                        _data = Logical.Authorization(JsonReadData.ReadUser(Logical.GetRequestBody(_server.GetRequest() ?? null!)));
                        break;
                }

                string csvData = ICsvString.CsvReadString(_data);
                ((IResponseSend)this).Send(csvData, response);

                Console.WriteLine($"Запрос от {_request.UserHostAddress} обработан");

            }
        }
    }
}
