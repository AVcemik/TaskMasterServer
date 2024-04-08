﻿using System.Net;
using System.Web;
using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
using TaskMasterServer.DataBase.Service;
using TaskMasterServer.Service.Csv;

namespace TaskMasterServer.Service.HTTP
{
    internal class Server
    {
        private HttpListener server = new HttpListener();
        public Server()
        {
            server.Prefixes.Add($"http://*:{8080}/");
            server.Prefixes.Add($"http://+:{8080}/");
        }
        public Server(string port)
        {
            server.Prefixes.Add($"http://*:{port}/");
            server.Prefixes.Add($"http://+:{port}/");
        }

        public void Start()
        {
            server.Start();
            Console.WriteLine("Сервер запущен");
        }

        public async void QueryProcessing()
        {
            HttpListenerContext context = await server.GetContextAsync();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            Console.WriteLine(request.RawUrl);
            Console.WriteLine(request.ToString());
            Console.WriteLine(request.Url);

            var query = HttpUtility.ParseQueryString(request.Url!.Query);

            if (query["response"] == "ListTask".ToLower())
            {
                User user = new();
                foreach (var item in DataBd.ReadUser())
                {
                    if (item.UserId == int.Parse(query["userId"]))
                    {
                        user = item;
                    }
                }
                var token = query["token"];
                List<DataBase.Task> userTaskBd = DataBd.ReadTask().Where(t => t.DepartmentId == int.Parse(user.DepartmentId.ToString()!)).ToList();

                //Создать отдельный класс
                List<TaskUser> userTask = new List<TaskUser>();
                foreach (var item in userTaskBd)
                {
                    userTask.Add(new TaskUser(item.TaskId, item.TaskName, item.Description, item.DateCreate, item.Deadline, item.Status.StatusType, item.Priority.PriorityType));
                }

                string csvData = ICsvString.CsvReadString(userTask);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvData);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);
                output.Close();
                Console.WriteLine($"{request.Url} - Обработан");
            }
        }
    }
}
