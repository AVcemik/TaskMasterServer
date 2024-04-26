using System.Text;
using System.Text.Encodings.Web;
using System.Text.Json;
using TaskMasterServer.Service.Business.CRUD;
using TaskMasterServer.Service.Csv;
using TaskMasterServer.Service.HTTP;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.Business
{
    internal class InWorkServer
    {
        private Server _server;
        private Data.Data? _data;
        private bool _isWhileContinue = false;
        public InWorkServer(bool prefixServer)
        {
            _server = new Server(prefixServer);
            _data = new Data.Data();
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

                // Авторизация
                if (_server.GetContentType()!.ToLower() == RequestType.Authorization.GetDescription().ToLower())
                {
                    _data = AuthorizationUser.Login(JsonReadData.ReadUser(_server.GetRequestBody()));

                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                    };
                    string result = JsonSerializer.Serialize(_data, options);
                    _server.Send(result, _server.GetResponse()!);
                }

                // Добавление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.AddTask.GetDescription().ToLower())
                {
                    string result = CreateData.CreateTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);

                }

                // Добавление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.AddUser.GetDescription().ToLower())
                {
                    string result = CreateData.CreateUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
                }

                // Добавление департамента
                else if (_server.GetContentType()!.ToLower() == RequestType.AddDepartment.GetDescription().ToLower())
                {
                    string result = CreateData.CreateDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
                }

                // Добавление Приоритета
                else if (_server.GetContentType()!.ToLower() == RequestType.AddPrioritet.GetDescription().ToLower())
                {
                    string result = CreateData.CreatePrioritet(JsonReadData.ReadPrioritet(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
                }

                // Добавление статуса
                else if (_server.GetContentType()!.ToLower() == RequestType.AddStatus.GetDescription().ToLower())
                {
                    string result = CreateData.CreateStatus(JsonReadData.ReadStatus(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
                }

                // Обновление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.AddTask.GetDescription().ToLower())
                {
                    string result = UpdateData.UpdateTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);

                }

                // Обновление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.AddUser.GetDescription().ToLower())
                {
                    string result = UpdateData.UpdateUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
                }

                // Обновление департамента
                else if (_server.GetContentType()!.ToLower() == RequestType.AddDepartment.GetDescription().ToLower())
                {
                    string result = UpdateData.UpdateDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
                }

                // Обновление Приоритета
                else if (_server.GetContentType()!.ToLower() == RequestType.AddPrioritet.GetDescription().ToLower())
                {
                    string result = UpdateData.UpdatePrioritet(JsonReadData.ReadPrioritet(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
                }

                // Обновление статуса
                else if (_server.GetContentType()!.ToLower() == RequestType.AddStatus.GetDescription().ToLower())
                {
                    string result = UpdateData.UpdateStatus(JsonReadData.ReadStatus(_server.GetRequestBody()));
                    _server.Send(result, _server.GetResponse()!);
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
