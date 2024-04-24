using CsvHelper.Configuration;
using CsvHelper;
using System.Globalization;
using System.Text;
using taskMasterClientTest.Data;
using System.Text.Json;
using taskMasterClientTest.Service.Enums;
using System.ComponentModel;

namespace taskMasterClientTest.Service
{
    
    internal class User
    {
        public UserData CurrentUser = new UserData();
        List<UserData> Users = new List<UserData>();
        List<TaskData> Tasks = new List<TaskData>();
        string IpConnection { get; set; }
        HttpContent content;
        HttpResponseMessage response;

        public User(string ipConnection) 
        {
            IpConnection = ipConnection;
        }

        public void Login(HttpClient client, string login, string password)
        {
            

            UserData user = new UserData() { Login = login, Password = password };
            string messageUser = JsonSerializer.Serialize<UserData>(user);

            content = new StringContent(messageUser, Encoding.UTF8, RequestType.Authorization.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            if (response.IsSuccessStatusCode)
            {

                try
                {
                    CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);
                    StreamReader reader = new StreamReader(response.Content.ReadAsStream(), Encoding.Unicode);

                    string[] dataUserTask = reader.ReadToEnd().Split('^');

                    TextReader usersReader = new StringReader(dataUserTask[0]);
                    TextReader tasksReder = new StringReader(dataUserTask[1]);


                    using (CsvReader csvReader = new CsvReader(usersReader, csvConfig))
                    {
                        Users = csvReader.GetRecords<UserData>().ToList();
                    }

                    using (CsvReader csvReader = new CsvReader(tasksReder, csvConfig))
                    {
                        Tasks = csvReader.GetRecords<TaskData>().ToList();
                    }

                    reader.Close();
                    usersReader.Close();
                    tasksReder.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Ошибка в потоках или csv");
                    Console.WriteLine(ex.ToString());
                }
                
            }
            else { Console.WriteLine("Ответ от сервера: " + response.StatusCode); }
        }
        public void CreateTask(HttpClient client)
        {
            TaskData task = new TaskData("Сервер тест", "Описание", DateTime.Now, DateTime.Now, "Айтишники", "Низкий");
            string messageTask = JsonSerializer.Serialize<TaskData>(task);

            content = new StringContent(messageTask, Encoding.UTF8, RequestType.AddTask.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void CreateUser(HttpClient client)
        {
            UserData user = new UserData("имя", "фамилия", DateTime.Now, "+7 123 456 78 90", "логин", "пароль", "Айтишники");
            string message = JsonSerializer.Serialize<UserData>(user);
            message += "^false";

            content = new StringContent(message, Encoding.UTF8, RequestType.AddUser.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void CreateDepartment(HttpClient client)
        {
            DepartmentData department = new DepartmentData("Департамент");
            string message = JsonSerializer.Serialize<DepartmentData>(department);

            content = new StringContent(message, Encoding.UTF8, RequestType.AddDepartment.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void Display()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Пользователи: ");
            foreach (UserData user in Users)
            {
                Console.WriteLine($"{user.Id} - {user.FirstName} - {user.LastName} - {user.Birthday} - {user.ContactPhone} - {user.Login} - {user.Password} - {user.Email} - {user.Department} - {user.IsResponsible}");
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Задачи отдела пользователя:");
            foreach (TaskData task in Tasks)
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
            StreamReader reader = new StreamReader(response.Content.ReadAsStream(), Encoding.Unicode);
            string responseContent = reader.ReadToEnd();
            reader.Close();
            return responseContent;
        }
    }
}
