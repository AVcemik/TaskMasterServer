using TaskMasterServer.Service.Business;
using TaskMasterServer.Service.Business.CRUD;
using TaskMasterServer.Service.Encryptions;
using TaskMasterServer.Service.HTTP;
using TaskMasterServer.Service.JSON;

namespace TaskMasterServer.Service
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

        /// <summary>
        /// Старт сервера
        /// </summary>
        public void Start()
        {
            _server.Start();
        }

        /// <summary>
        /// Процесс принятия запросов (цикл сервера)
        /// </summary>
        public void InWorkProcess()
        {
            while (true)
            {
                string? result = null;
                _server.QueryProcessing(out _isWhileContinue);

                if (_isWhileContinue) continue;

                // Авторизация
                if (_server.GetContentType()!.ToLower() == RequestType.Authorization.GetDescription().ToLower())
                {
                    _data = AuthorizationUser.Login(JsonReadData.ReadUser(_server.GetRequestBody()));
                    result = JsonWriteData.WriteData(_data);
                }

                // Добавление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.AddTask.GetDescription().ToLower())
                {
                    result = CreateData.CreateTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                }

                // Добавление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.AddUser.GetDescription().ToLower())
                {
                    result = CreateData.CreateUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                }

                // Добавление департамента
                else if (_server.GetContentType()!.ToLower() == RequestType.AddDepartment.GetDescription().ToLower())
                {
                    result = CreateData.CreateDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                }

                // Добавление Приоритета
                else if (_server.GetContentType()!.ToLower() == RequestType.AddPrioritet.GetDescription().ToLower())
                {
                    result = CreateData.CreatePrioritet(JsonReadData.ReadPrioritet(_server.GetRequestBody()));
                }

                // Добавление статуса
                else if (_server.GetContentType()!.ToLower() == RequestType.AddStatus.GetDescription().ToLower())
                {
                    result = CreateData.CreateStatus(JsonReadData.ReadStatus(_server.GetRequestBody()));
                }

                // Обновление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateTask.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                }

                // Обновление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateUser.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                }

                // Обновление департамента
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateDepartment.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                }

                // Обновление Приоритета
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdatePrioritet.GetDescription().ToLower())
                {
                    result = UpdateData.UpdatePrioritet(JsonReadData.ReadPrioritet(_server.GetRequestBody()));
                }

                // Обновление статуса
                else if (_server.GetContentType()!.ToLower() == RequestType.UpdateStatus.GetDescription().ToLower())
                {
                    result = UpdateData.UpdateStatus(JsonReadData.ReadStatus(_server.GetRequestBody()));
                }

                // Удаление задачи
                else if (_server.GetContentType()!.ToLower() == RequestType.DeleteTask.GetDescription().ToLower())
                {
                    result = DeleteData.DeleteTask(JsonReadData.ReadTask(_server.GetRequestBody()));
                }

                // Удаление пользователя
                else if (_server.GetContentType()!.ToLower() == RequestType.DeleteUser.GetDescription().ToLower())
                {
                    result = DeleteData.DeleteUser(JsonReadData.ReadUser(_server.GetRequestBody()));
                }

                // Удаление департамента
                else if (_server.GetContentType()!.ToLower() == RequestType.DeleteDepartment.GetDescription().ToLower())
                {
                    result = DeleteData.DeleteDepartment(JsonReadData.ReadDepartment(_server.GetRequestBody()));
                }

                // Удаление Статуса
                else if (_server.GetContentType()!.ToLower() == RequestType.DeleteStatus.GetDescription().ToLower())
                {
                    result = DeleteData.DeleteStatus(JsonReadData.ReadStatus(_server.GetRequestBody()));
                }

                // Удаление Приоритета
                else if (_server.GetContentType()!.ToLower() == RequestType.DeletePrioritet.GetDescription().ToLower())
                {
                    result = DeleteData.DeletePrioritet(JsonReadData.ReadPrioritet(_server.GetRequestBody()));
                }

                // Если есть результат, то отправляем его клиенту
                if (result != null)
                {
                    result = Encryption.EncryptString(result);
                    _server.Send(result, _server.GetResponse()!);
                    Console.WriteLine("Запрос принят\n");
                }
                else
                {
                    Console.WriteLine($"Запрос проигнорирован...\n");
                }
            }
        }
    }
}
