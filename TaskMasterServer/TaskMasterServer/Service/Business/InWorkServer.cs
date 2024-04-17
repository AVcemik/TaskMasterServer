using TaskMasterServer.Service.Csv;
using TaskMasterServer.Service.HTTP;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.Business
{
    internal class InWorkServer
    {
        private Server _server = new Server();
        private int _count = 1;
        private Data.Data?_data;
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
                    case "application/auth": _data = UserQuery.Authorization(JsonReadData.ReadUser(_server.GetRequestBody()));
                        break;
                }

                string csvData = ICsvString.CsvWriteString(_data);
                _server.Send(csvData, _server.GetResponse()!);

                Console.WriteLine($"Запрос от {_server.GetRequest()!.UserHostAddress} обработан");

            }
        }
    }
}
