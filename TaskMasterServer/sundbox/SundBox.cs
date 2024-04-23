using CsvHelper;
using CsvHelper.Configuration;
using sundbox;
using System.Globalization;
using System.Text;
using System.Text.Json;
using Tasks = sundbox.Task;

string postServer = "http://176.123.160.24:8080";
string postLocal = "http://localhost:8080";

List<User> users = new List<User>();
List<Tasks> tasks = new List<Tasks>();


Console.WriteLine("Нажмите любую клавишу для продолжения начала работы приложения...");
Console.ReadKey();

HttpContent content;
HttpResponseMessage response;

using (HttpClient client = new HttpClient())
{
    Login(client);
    DisplayListTasks();

    //ReadDataLogin(client);
    //AddTaskData(client);
}

Console.WriteLine("Нажмите любую клаавишу для завершения....");
Console.ReadKey();

void ReadDataLogin(HttpClient client)
{
    User user = new User() { Login = "it1", Password = "1" };
    string messageUser = JsonSerializer.Serialize<User>(user);

    content = new StringContent(messageUser, Encoding.UTF8, "Application/Authorization");
    response = client.PostAsync(postLocal, content).Result;

    if (response.IsSuccessStatusCode)
    {
        string responseContent = response.Content.ReadAsStringAsync().Result;

        Console.WriteLine("Ответ от сервера: " + responseContent);
    }
    else
    {
        Console.WriteLine("Ошибка: " + response.StatusCode);
    }
}
void Login(HttpClient client)
{
    User user = new User() { Login = "it1", Password = "1" };
    string messageUser = JsonSerializer.Serialize<User>(user);

    content = new StringContent(messageUser, Encoding.UTF8, "Application/Authorization");
    response = client.PostAsync(postLocal, content).Result;

    if (response.IsSuccessStatusCode)
    {
        string responseContent = response.Content.ReadAsStringAsync().Result;
        string[] dataUserTasks = responseContent.Split("^");

        CsvConfiguration csvConfig = new CsvConfiguration(CultureInfo.InvariantCulture);


        using (StringReader reader = new StringReader(dataUserTasks[1]))
        using (CsvReader csvReader = new CsvReader(reader, csvConfig))
        {
            tasks = csvReader.GetRecords<Tasks>().ToList();
        }
    }
    else { Console.WriteLine("Ответ от сервера: " + response.StatusCode); }     
}
void AddTaskData(HttpClient client)
{
    Tasks task = new Tasks("Новая задача", "Описание", DateTime.Now, DateTime.Now, "Айтишники", "Завершена", "Низкий");
    string messageTask = JsonSerializer.Serialize<Tasks>(task);

    content = new StringContent(messageTask, Encoding.UTF8, "application/taskadd");
    response = client.PostAsync(postLocal, content).Result;
    if (response.IsSuccessStatusCode)
    {
        string responseContent = response.Content.ReadAsStringAsync().Result;

        Console.WriteLine("Ответ от сервера: " + responseContent);
    }
    else
    {
        Console.WriteLine("Ошибка: " + response.StatusCode);
    }
}
void UpdateTaskData(HttpClient client)
{

}
void DisplayListTasks()
{
    foreach (Tasks task in tasks)
    {
        Console.WriteLine($"{task.Id} - {task.Title} - {task.Description} - {task.StartDate} - {task.DeadLine} - {task.Department} - {task.Status} - {task.Priority}");
    }
}



