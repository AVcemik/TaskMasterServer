using TaskMasterServer.Service.Csv;
using TaskMasterServer.Service.HTTP;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.Business
{
    internal class InWorkServer
    {
        private Server _server = new Server();
        private Data.Data?_data;
        private bool _isWhileContinue = false;
        public void Start()
        {
            _server.Start();
        }
        public void InWorkProcess()
        {
            while (true)
            {
                _server.QueryProcessing(out _isWhileContinue);

                if (_isWhileContinue) continue;

                if (_server.GetContentType()!.ToLower() == "Application/Auth".ToLower())
                {
                    _data = UserQuery.Authorization(JsonReadData.ReadUser(_server.GetRequestBody()));

                    string csvData = ICsvString.CsvWriteString(_data);
                    _server.Send(csvData, _server.GetResponse()!);
                }
                else if (_server.GetContentType()!.ToLower() == "Application/TaskAdd".ToLower())
                {
                    string result =  UserQuery.TaskAdd(JsonReadData.ReadTask(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);

                }

                Console.WriteLine($"Запрос от {_server.GetRequest()!.UserHostAddress} обработан");

                //switch (_server.GetContentType()!.ToLower())
                //{
                //    case "application/auth": _data = UserQuery.Authorization(JsonReadData.ReadUser(_server.GetRequestBody()));
                //        break;
                //    case "application/taskadd": UserQuery.TaskAdd(JsonReadData.ReadTask(_server.GetRequestBody()));
                //        break;
                //}

            }
        }
    }
}
