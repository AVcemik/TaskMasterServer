using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;
using taskMasterClientTest.Data;
using System.Text.Json;
using taskMasterClientTest.Service.Enums;
using System.ComponentModel;
using static System.Net.Mime.MediaTypeNames;
using System.Text.Encodings.Web;

namespace taskMasterClientTest.Service
{
    
    internal class User
    {
        public UserDatas CurrentUser = new UserDatas();
        public Data.Data Data = new Data.Data();
        //List<UserData> Users = new List<UserData>();
        //List<TaskData> Tasks = new List<TaskData>();
        string IpConnection { get; set; }
        HttpContent content;
        HttpResponseMessage response;

        public User(string ipConnection) 
        {
            IpConnection = ipConnection;
        }

        public void Login(HttpClient client, string login, string password)
        {
            Data.Data data = new Data.Data();

            UserDatas user = new UserDatas() { Login = login, Password = password };
            string messageUser = JsonSerializer.Serialize<UserDatas>(user);

            content = new StringContent(messageUser, Encoding.UTF8, RequestType.Authorization.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            if (response.IsSuccessStatusCode)
            {
                string? content = null;

                using (StreamReader reader = new StreamReader(response.Content.ReadAsStream(), Encoding.Unicode))
                {
                    JsonSerializerOptions options = new JsonSerializerOptions()
                    {
                        WriteIndented = true,
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping

                    };
                    Data = JsonSerializer.Deserialize<Data.Data>(reader.ReadToEnd(), options);
                }
            }
            //    if (response.IsSuccessStatusCode)
            //    {

            //        try
            //        {
            //            CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
            //            StreamReader reader = new StreamReader(response.Content.ReadAsStream(), Encoding.Unicode);

            //            string[] dataUserTask = reader.ReadToEnd().Split('^');

            //            TextReader usersReader = new StringReader(dataUserTask[0]);
            //            TextReader tasksReder = new StringReader(dataUserTask[1]);


            //            using (CsvReader csvReader = new CsvReader(usersReader, csvConfig))
            //            {
            //                Users = csvReader.GetRecords<UserData>().ToList();
            //            }

            //            using (CsvReader csvReader = new CsvReader(tasksReder, csvConfig))
            //            {
            //                Tasks = csvReader.GetRecords<TaskData>().ToList();
            //            }

            //            reader.Close();
            //            usersReader.Close();
            //            tasksReder.Close();
            //        }
            //        catch (Exception ex)
            //        {
            //            Console.WriteLine("Ошибка в потоках или csv");
            //            Console.WriteLine(ex.ToString());
            //        }

            //    }
            else { Console.WriteLine("Ответ от сервера: " + response.StatusCode); }
        }
        public void CreateTask(HttpClient client)
        {
            TaskDatas task = new TaskDatas("Сервер тест", "Описание", DateTime.Now, DateTime.Now, "Айтишники", "Низкий");
            string messageTask = JsonSerializer.Serialize<TaskDatas>(task);

            content = new StringContent(messageTask, Encoding.UTF8, RequestType.AddTask.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void CreateUser(HttpClient client)
        {
            bool? isAdmin = false;
            UserDatas user = new UserDatas("имя", "фамилия", DateTime.Now, "+7 123 456 78 90", "логин", "пароль", "test@taskmaster.com", "Айтишники", isAdmin);
            string message = JsonSerializer.Serialize<UserDatas>(user);

            content = new StringContent(message, Encoding.UTF8, RequestType.AddUser.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void CreateDepartment(HttpClient client)
        {
            DepartmentDatas department = new DepartmentDatas("Департамент");
            string message = JsonSerializer.Serialize<DepartmentDatas>(department);

            content = new StringContent(message, Encoding.UTF8, RequestType.AddDepartment.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void Display()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Пользователи: ");
            foreach (UserDatas user in Data.Users)
            {
                Console.WriteLine($"{user.Id} - {user.FirstName} - {user.LastName} - {user.Birthday} - {user.ContactPhone} - {user.Login} - {user.Password} - {user.Email} - {user.Department} - {user.IsResponsible}");
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Задачи отдела пользователя:");
            foreach (TaskDatas task in Data.Tasks)
            {
                Console.WriteLine($"{task.Id} - {task.Title} - {task.Description} - {task.StartDate} - {task.DeadLine} - {task.Department} - {task.Status} - {task.Priority}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
        }
        
        public void DisplayResultResponse()
        {
            if (response.IsSuccessStatusCode)
                Console.WriteLine("Ответ от сервера: " + GetResponseContent());
            else
                ResponseError();
        }
        public void ResponseError()
        {
            Console.WriteLine("Ответ от сервера: " + GetResponseContent());
            Console.WriteLine("Ошибка: " + response.StatusCode);
        }
        public string GetResponseContent()
        {
            return response.Content.ReadAsStringAsync().Result;
            //StreamReader reader = new StreamReader(response.Content.ReadAsStream(), Encoding.Unicode);
            //string responseContent = reader.ReadToEnd();
            //reader.Close();
            //return responseContent;
        }

        public void DisplayAllData()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Пользователи: ");
            foreach (UserDatas user in Data.Users)
            {
                Console.WriteLine($"{user.Id} - {user.FirstName} - {user.LastName} - {user.Birthday} - {user.ContactPhone} - {user.Login} - {user.Password} - {user.Email} - {user.Department} - {user.IsResponsible}");
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Задачи отдела пользователя:");
            foreach (TaskDatas task in Data.Tasks)
            {
                Console.WriteLine($"{task.Id} - {task.Title} - {task.Description} - {task.StartDate} - {task.DeadLine} - {task.Department} - {task.Status} - {task.Priority}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Список отделов:");
            foreach (DepartmentDatas department in Data.Departments)
            {
                Console.WriteLine($"{department.Id} - {department.Name}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Список отделов:");
            foreach (PriorityDatas priritet in Data.Priorities)
            {
                Console.WriteLine($"{priritet.Id} - {priritet.PriorityType}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Список отделов:");
            foreach (StatusDatas status in Data.Statuses)
            {
                Console.WriteLine($"{status.Id} - {status.StatusType}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
