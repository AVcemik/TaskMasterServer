using System.Text;
using taskMasterClientTest.Data;
using System.Text.Json;
using taskMasterClientTest.Service.Enums;

using System.Text.Encodings.Web;

namespace taskMasterClientTest.Service
{
    
    internal class User
    {
        public UserDatas CurrentUser = new UserDatas();
        public Data.Data CurrentData = new Data.Data();

        string IpConnection { get; set; }
        HttpContent content;
        HttpResponseMessage response;

        JsonSerializerOptions jsonOptions = new JsonSerializerOptions();

        public User(string ipConnection) 
        {
            IpConnection = ipConnection;

            jsonOptions.WriteIndented = true;
            jsonOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
            jsonOptions.Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping;
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
                    CurrentData = JsonSerializer.Deserialize<Data.Data>(reader.ReadToEnd(), jsonOptions);
                }
            }
            else { Console.WriteLine("Ответ от сервера: " + response.StatusCode); }
        }
        public void CreateTask(HttpClient client)
        {
            TaskDatas task = new TaskDatas("Сервер тест", "Описание", DateTime.Now, DateTime.Now, "Айтишники", "Создана", "Низкий");
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
        public void UpdateTask(HttpClient client)
        {
            TaskDatas task = new TaskDatas(1007, "Старая задача", "Описание", DateTime.Now, DateTime.Now, "Айтишники", "Завершена", "Высокий");
            string messageTask = JsonSerializer.Serialize<TaskDatas>(task);

            content = new StringContent(messageTask, Encoding.UTF8, RequestType.UpdateTask.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void DeleteTask(HttpClient client)
        {
            TaskDatas task = CurrentData.Tasks.Where(t => t.Title == "Старая задача").First();
            string messageTask = JsonSerializer.Serialize<TaskDatas>(task);

            content = new StringContent(messageTask, Encoding.UTF8, RequestType.DeleteTask.GetDescription());
            response = client.PostAsync(IpConnection, content).Result;

            DisplayResultResponse();
        }
        public void Display()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Пользователи: ");
            foreach (UserDatas user in CurrentData.Users)
            {
                Console.WriteLine($"{user.Id} - {user.FirstName} - {user.LastName} - {user.Birthday} - {user.ContactPhone} - {user.Login} - {user.Password} - {user.Email} - {user.Department} - {user.IsResponsible}");
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Задачи отдела пользователя:");
            foreach (TaskDatas task in CurrentData.Tasks)
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
            string? result = null;
            using (StreamReader reader = new StreamReader(response.Content.ReadAsStream(), Encoding.Unicode))
            {
                result = reader.ReadToEnd();
            }
            return result;
        }
        public void DisplayAllData()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Пользователи: ");
            foreach (UserDatas user in CurrentData.Users)
            {
                Console.WriteLine($"{user.Id} - {user.FirstName} - {user.LastName} - {user.Birthday} - {user.ContactPhone} - {user.Login} - {user.Password} - {user.Email} - {user.Department} - {user.IsResponsible}");
            }

            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Задачи отдела пользователя:");
            foreach (TaskDatas task in CurrentData.Tasks)
            {
                Console.WriteLine($"{task.Id} - {task.Title} - {task.Description} - {task.StartDate} - {task.DeadLine} - {task.Department} - {task.Status} - {task.Priority}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Список отделов:");
            foreach (DepartmentDatas department in CurrentData.Departments)
            {
                Console.WriteLine($"{department.Id} - {department.Name}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Список отделов:");
            foreach (PriorityDatas priritet in CurrentData.Priorities)
            {
                Console.WriteLine($"{priritet.Id} - {priritet.PriorityType}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Список отделов:");
            foreach (StatusDatas status in CurrentData.Statuses)
            {
                Console.WriteLine($"{status.Id} - {status.StatusType}");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine();
        }
    }
}
