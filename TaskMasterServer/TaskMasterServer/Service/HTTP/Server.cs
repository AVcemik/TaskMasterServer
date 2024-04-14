using System.Net;
using System.Text.Json;
using TaskMasterServer.Data;
using TaskMasterServer.DataBase;
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

            Console.WriteLine("Request.RawUrl: " + request.RawUrl);
            Console.WriteLine("Request Headers: " + request.Headers);
            Console.WriteLine("Request.Url: " + request.Url);
            Console.WriteLine("Response Headers: " + response.Headers);


            Console.WriteLine("Request Headers: " + request.ContentType.Split(';').ToList()[0]);
            string requestBody;
            using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                requestBody = reader.ReadToEnd();
            }

            //var query = HttpUtility.ParseQueryString(request.Url!.Query);
            string[] auth = requestBody.Split('^');
            Console.WriteLine("RequestBody:");
            foreach (var item in auth)
            {
                Console.WriteLine(item);
            }


            if (request.ContentType.Split(';').ToList()[0] == "application/auth".ToLower())
            {
                Data.User user = JsonSerializer.Deserialize<Data.User>(requestBody);
                DataBase.User userBD = new();
                foreach (var item in DataBd.ReadUser())
                {
                    if (item.Login == user.Login)
                    {
                        userBD = item;
                    }
                }

                if (userBD.Password != user.Password)
                {

                }
                //var token = query["token"];
                List<DataBase.Task> userTaskBd = DataBd.ReadTask().Where(t => t.DepartmentId == int.Parse(userBD.DepartmentId.ToString()!)).ToList();

                //Создать отдельный класс

                List<TaskUser> userTask = new List<TaskUser>();
                foreach (var item in userTaskBd)
                {
                    userTask.Add(new TaskUser(item.TaskId, item.TaskName, item.Description, item.DateCreate, item.Deadline, item.Status!.StatusType, item.Priority!.PriorityType));
                }
                List<Data.User> users = new List<Data.User>();
                users.Add(new Data.User(userBD.UserId, userBD.Firstname, userBD.Lastname, userBD.Brithday, userBD.Contactphone, userBD.Login, userBD.Password, userBD.Department.DepartmentName, userBD.Isresponsible));

                var csvData = ICsvString.CsvReadString(userTask, users);

                byte[] buffer = System.Text.Encoding.Unicode.GetBytes(csvData);
                response.ContentLength64 = buffer.Length;
                Stream output = response.OutputStream;
                output.Write(buffer, 0, buffer.Length);

                output.Close();
                Console.WriteLine($"{request.Url} - Обработан");
            }
        }
    }
}
