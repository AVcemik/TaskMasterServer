using TaskMasterServer.Service.Business.CRUD;
using TaskMasterServer.Service.Csv;
using TaskMasterServer.Service.HTTP;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.Business
{
    internal class InWorkServer
    {
        private Server _server;
        private Data.Data?_data;
        private bool _isWhileContinue = false;
        public InWorkServer(bool prefixServer)
        {
            _server = new Server(prefixServer);
        }
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

                //Проверяем что от нас хотят
                if (_server.GetContentType()!.ToLower() == RequestType.Authorization.ToString().ToLower())
                {
                    _data = Authorization.Login(JsonReadData.ReadUser(_server.GetRequestBody()));

                    string csvData = ICsvString.CsvWriteString(_data);
                    _server.Send(csvData, _server.GetResponse()!);
                }
                else if (_server.GetContentType()!.ToLower() == RequestType.AddTask.ToString().ToLower())
                {
                    CreateData.CreateTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                    string result = "Задача успешно создана";
                    _server.Send(result, _server.GetResponse()!);

                }
                else if (_server.GetContentType()!.ToLower() == RequestType.AddUser.ToString().ToLower())
                {
                    var UserdataAndBool = JsonReadData.ReadUserAndIsAdmin(_server.GetRequestBody());
                    CreateData.CreateUser(UserdataAndBool.Item1, UserdataAndBool.Item2);
                    string result = "Пользователь успешно добавлен";
                    _server.Send(result, _server.GetResponse()!);
                }
                else if (_server.GetContentType()!.ToLower() == RequestType.AddDepartment.ToString().ToLower())
                {
                    CreateData.CreateDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                }

                Console.WriteLine($"Клиент: {_server.GetRequest()!.UserHostAddress}\nЗапрос: {_server.GetContentType()!} - Обработан");

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
