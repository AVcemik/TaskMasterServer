using CsvHelper;
using sundbox;
using System.Text;
using System.Text.Json;
using Task = sundbox.Task;

string postServer = "http://176.123.160.24:8080";
string postLocal = "http://localhost:8080";

Console.WriteLine("Нажмите любую клавишу для продолжения начала работы приложения...");
Console.ReadKey();

HttpContent content;
HttpResponseMessage response;



using (HttpClient client = new HttpClient())
{
    ReadDataLogin(client);
    AddTaskData(client);


}

Console.WriteLine("Нажмите любую клаавишу для завершения....");
Console.ReadKey();

async void ReadDataLogin(HttpClient client)
{
    User user = new User() { Login = "it1", Password = "1" };
    string messageUser = JsonSerializer.Serialize<User>(user);

    content = new StringContent(messageUser, Encoding.UTF8, "application/auth");
    response = await client.PostAsync(postLocal, content);

    if (response.IsSuccessStatusCode)
    {
        string responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Ответ от сервера: " + responseContent);
    }
    else
    {
        Console.WriteLine("Ошибка: " + response.StatusCode);
    }
}
async string Login(HttpClient client)
{
    User user = new User() { Login = "it1", Password = "1" };
    string messageUser = JsonSerializer.Serialize<User>(user);

    content = new StringContent(messageUser, Encoding.UTF8, "application/taskupdate");
    response = await client.PostAsync(postLocal, content);

    if (response.IsSuccessStatusCode)
    {
        string responseContent = await response.Content.ReadAsStringAsync();
        string[] dataUserTasks = responseContent.Split("^");

        using (StringReader reader = new StringReader(dataUserTasks[1]))
        using (CsvReader csvReader = new CsvReader(reader, new csvConfi))
        {

        }
    }
    else { Console.WriteLine("Ответ от сервера: " + response.StatusCode); }
}
async void AddTaskData(HttpClient client)
{
    Task task = new Task("Новая задача", "Описание", DateTime.Now, DateTime.Now, "Айтишники", "Завершена", "Низкий");
    string messageTask = JsonSerializer.Serialize<Task>(task);

    content = new StringContent(messageTask, Encoding.UTF8, "application/taskadd");
    response = await client.PostAsync(postLocal, content);
    if (response.IsSuccessStatusCode)
    {
        string responseContent = await response.Content.ReadAsStringAsync();

        Console.WriteLine("Ответ от сервера: " + responseContent);
    }
    else
    {
        Console.WriteLine("Ошибка: " + response.StatusCode);
    }
}
async void UpdateTaskData(HttpClient client)
{

}


