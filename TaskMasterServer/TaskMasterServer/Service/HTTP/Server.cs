using System.Net;
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
            string[] tempKeyValuy = new string[2];
            tempKeyValuy = auth[0].Split('=');
            if (request.ContentType.Split(';').ToList()[0] == "application/auth".ToLower())
            {
                DataBase.User userBD = new();
                tempKeyValuy = auth[1].Split('=');
                foreach (var item in DataBd.ReadUser())
                {
                    if (item.Login == tempKeyValuy[1])
                    {
                        userBD = item;
                    }
                }
                tempKeyValuy = auth[2].Split('=');
                if (userBD.Password != tempKeyValuy[1])
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
