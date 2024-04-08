var client = new HttpClient();
//string id = "2";
//var response = await client.GetAsync($"http://localhost:8080/task?userId={id}");

while (true)
{
    Console.Write("Введите запрос: ");
    string query = Console.ReadLine()!;
    var response = await client.GetAsync(query);

    if (response.IsSuccessStatusCode)
    {
        var content = await response.Content.ReadAsStringAsync();
        Console.WriteLine("Ваши задачи: ");
        Console.WriteLine(content);
    }
    else
    {
        Console.WriteLine($"Ошибка: {response.StatusCode}");
    }
    Console.WriteLine("Нажмите любую клавишу для продолжения...");
    Console.ReadKey();
    Console.Clear();
}

