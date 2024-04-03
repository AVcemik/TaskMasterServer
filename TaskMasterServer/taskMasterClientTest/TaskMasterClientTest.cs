HttpClient client = new HttpClient();

    try
    {
        var response = await client.GetAsync("http://127.0.0.1:8888/GetTask/");
        response.EnsureSuccessStatusCode();
        var csvData = await response.Content.ReadAsStringAsync();

        Console.WriteLine(csvData);
    }
    catch (HttpRequestException e)
    {
        Console.WriteLine($"Ошибка запроса: {e.Message}");
    }