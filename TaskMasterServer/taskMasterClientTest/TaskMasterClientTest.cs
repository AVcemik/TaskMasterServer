var client = new HttpClient();
string id = "2";
var response = await client.GetAsync($"http://localhost:8888/task?userId={id}");

if (response.IsSuccessStatusCode)
{
    var content = await response.Content.ReadAsStringAsync();
    Console.WriteLine(content);
}
else
{
    Console.WriteLine($"Ошибка: {response.StatusCode}");
}
