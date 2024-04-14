using System.Text;

Console.ReadKey();
string message = "response=auth^login=it1^password=password1";
using (HttpClient client = new HttpClient())
{
    HttpContent content = new StringContent(message, Encoding.UTF8, "application/auth");
    Console.WriteLine("httpcontent - прошли");
    HttpResponseMessage response = await client.PostAsync("http://localhost:8080", content);
    Console.WriteLine("httpResponseMessage - прошли");

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine("IsSuccessStatusCode - прошли");
        string responseContent = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Ответ от сервера: " + responseContent);
    }
    else
    {
        Console.WriteLine("Ошибка: " + response.StatusCode);
    }
}
Console.ReadKey();