using System.Net;
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

        public void QueryProcessing()
        {
            HttpListenerContext context = server.GetContext();
            HttpListenerRequest request = context.Request;
            HttpListenerResponse response = context.Response;

            Console.WriteLine(request.RawUrl);
            Console.WriteLine(request.ToString());
            Console.WriteLine(request.Url);

            var query = HttpUtility.ParseQueryString(request.Url!.Query);

            if (query["response"] == "Auth".ToLower())
            {
                DataBase.User userBD = new();
                foreach (var item in DataBd.ReadUser())
                {
                    if (item.Login == query["login"])
                    {
                        userBD = item;
                    }
                }
                if (userBD.Password != query["pass"])
                {

                }
                var token = query["token"];
                List<DataBase.Task> userTaskBd = DataBd.ReadTask().Where(t => t.DepartmentId == int.Parse(userBD.DepartmentId.ToString()!)).ToList();

                //Создать отдельный класс
                
                List<TaskUser> userTask = new List<TaskUser>();
                foreach (var item in userTaskBd)
                {
                    userTask.Add(new TaskUser(item.TaskId, item.TaskName, item.Description, item.DateCreate, item.Deadline, item.Status!.StatusType, item.Priority!.PriorityType));
                }
                List<Data.User> users = new List<Data.User>();
                users.Add(new Data.User(userBD.UserId, userBD.UserName, userBD.UserName, userBD.Login, userBD.Password, userBD.Department.DepartmentName));

                var csvData = ICsvString.CsvReadString(userTask, users);

                byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvData);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                foreach (var item in buffer)
                {
                    Console.Write(item);
                }

                output.Close();
                Console.WriteLine($"{request.Url} - Обработан");
            }
        }
    }
}
