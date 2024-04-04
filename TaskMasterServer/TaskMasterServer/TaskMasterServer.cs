using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Net;
using System.Web;
using TaskMasterServer.DataBase;
using TaskBD = TaskMasterServer.DataBase.Task;

List<TaskBD> tempTask = new();
List<User> tempUser = new();
List<Department> tempDepartment = new();

using (TaskUserDbContext dbContext = new TaskUserDbContext())
{
    tempTask = dbContext.Tasks!.ToList();
    tempUser = dbContext.Users!.ToList();
    tempDepartment = dbContext.Departments!.ToList();
}

HttpListener server = new HttpListener();
server.Prefixes.Add($"http://*:{8888}/");
server.Start();
Console.WriteLine("Сервер запущен");

while (true)
{
    HttpListenerContext context = await server.GetContextAsync();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;

    var query = HttpUtility.ParseQueryString(request.Url.Query);
    var userId = query["userId"];

    var userTask = tempUser.Where(t => t.UserId == int.Parse(userId!)).ToList();

    string csvData;
    using StringWriter writer = new StringWriter();
    using (CsvWriter csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
    {
        csvWriter.WriteRecords(userTask);
        csvData = writer.ToString();
    }

    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvData);
    response.ContentLength64 = buffer.Length;
    Stream output = response.OutputStream;
    output.Write(buffer, 0, buffer.Length);
    output.Close();
    Console.WriteLine($"{request.Url} - Обработан");
}


server.Stop();
server.Close();