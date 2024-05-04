using System.Text.Encodings.Web;
using System.Text.Json;
using TaskMasterServer.Service.Business.CRUD;
using TaskMasterServer.Service.Encryption;
using TaskMasterServer.Service.HTTP;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service.Business
{
    internal class InWorkServer
    {
        private Server _server;
        private Data.Data? _data;
        private bool _isWhileContinue = false;
        public InWorkServer(bool prefixServer, int port)
        {
            _server = new Server(prefixServer, port);
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
                string? result = null;

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
                    result = JsonSerializer.Serialize(_data, options);

                    //_server.Send(result, _server.GetResponse()!);
                }

                // Добавление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.AddTask.GetDescription().ToLower())
                {
                    result = CreateData.CreateTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Добавление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.AddUser.GetDescription().ToLower())
                {
                    result = CreateData.CreateUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                    
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Добавление департамента
                else if (_server.GetContentType()!.ToLower() == RequestType.AddDepartment.GetDescription().ToLower())
                {
                    result = CreateData.CreateDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Добавление Приоритета
                else if (_server.GetContentType()!.ToLower() == RequestType.AddPrioritet.GetDescription().ToLower())
                {
                    result = CreateData.CreatePrioritet(JsonReadData.ReadPrioritet(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Добавление статуса
                else if (_server.GetContentType()!.ToLower() == RequestType.AddStatus.GetDescription().ToLower())
                {
                    result = CreateData.CreateStatus(JsonReadData.ReadStatus(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Обновление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateTask.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);

                }

                // Обновление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateUser.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Обновление департамента
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateDepartment.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Обновление Приоритета
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdatePrioritet.GetDescription().ToLower())
                {
                    result = UpdateData.UpdatePrioritet(JsonReadData.ReadPrioritet(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Обновление статуса
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateStatus.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateStatus(JsonReadData.ReadStatus(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Удаление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.DeleteTask.GetDescription().ToLower())
                {
                    result = DeleteData.DeleteTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                // Удаление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.DeleteUser.GetDescription().ToLower())
                {
                    result = DeleteData.DeleteUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                    //_server.Send(result, _server.GetResponse()!);
                }

                if (result != null)
                {
                    result = Encryption.Encryption.EncryptString(result!, 5);
                    _server.Send(result, _server.GetResponse()!);
                }

                Console.WriteLine($"Клиент: {_server.GetRequest()!.UserHostAddress}\nЗапрос: {_server.GetContentType()!} - Обработан");
            }
        }
    }
}
