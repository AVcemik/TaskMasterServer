using CsvHelper;
using System.Net;
using System.Text;
using TaskMasterServer;


HttpListener server = new HttpListener();
Console.WriteLine("Сервер запущен");

using (TaskUserDbContext contex = new TaskUserDbContext())
{
    var tasks = contex.Tasks.ToList();

    CsvWriter csvWruter = new CsvWriter();

    byte[] buffer = Encoding.UTF8.GetBytes(tasks.ToString()!);
    string local = "http://127.0.0.1:8888/GetTask/";

    server.Prefixes.Add(local);

    server.Start();

    var context = await server.GetContextAsync();

    var request = context.Request;
    var response = context.Response;
    var user = context.User;

    response.ContentLength64 = buffer.Length;
    using Stream output = response.OutputStream;

    await output.WriteAsync(buffer);
    await output.FlushAsync();

    Console.WriteLine("Запрос обработан");
}

server.Stop();
server.Close();