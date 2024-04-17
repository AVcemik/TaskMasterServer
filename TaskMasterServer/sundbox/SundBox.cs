using System.Text;
using System.Text.Json;

Console.ReadKey();
User user = new User() { Login = "it1", Password = "password1" };
string message = JsonSerializer.Serialize<User>(user);
using (HttpClient client = new HttpClient())
{
    HttpContent content = new StringContent(message, Encoding.UTF8, "application/auth");
    HttpResponseMessage response = await client.PostAsync("http://176.123.160.24:8080", content);
    //HttpResponseMessage response = await client.PostAsync("http://localhost:8080", content);

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


class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime? Birthday { get; set; }
    public string? ContactPhone { get; set; }
    public string? Login { get; set; }
    public string? Password { get; set; }
    public string? Department { get; set; }
    public bool? IsResponsible { get; set; }
    public User() { }
}
