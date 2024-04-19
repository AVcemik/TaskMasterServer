using sundbox;
using System.Text;
using System.Text.Json;
using Task = sundbox.Task;

string postServer = "http://176.123.160.24:8080";
string postLocal = "http://localhost:8080";

Console.WriteLine("Нажмите любую клавишу для продолжения начала работы приложения...");
Console.ReadKey();

User user = new User() { Login = "it1", Password = "password1" };
string messageUser = JsonSerializer.Serialize<User>(user);

Task task = new Task("Новая задача", "Описание", DateTime.Now, DateTime.Now, "Айтишники","Завершена", "Низкий");
string messageTask = JsonSerializer.Serialize<Task>(task);

using (HttpClient client = new HttpClient())
{
    HttpContent content = new StringContent(messageUser, Encoding.UTF8, "application/auth");
    HttpResponseMessage response = await client.PostAsync(postLocal, content);

    if (response.IsSuccessStatusCode)
    {
        string responseContent = await response.Content.ReadAsStringAsync();
        
        Console.WriteLine("Ответ от сервера: " + responseContent);
    }
    else
    {
        Console.WriteLine("Ошибка: " + response.StatusCode);
    }

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
Console.ReadKey();


