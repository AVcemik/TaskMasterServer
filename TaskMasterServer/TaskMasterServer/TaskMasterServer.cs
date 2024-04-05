using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.IO;
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
server.Prefixes.Add($"http://*:{8080}/");
server.Prefixes.Add($"http://+:{8080}/");
server.Start();
Console.WriteLine("Сервер запущен");

while (true)
{
    HttpListenerContext context = await server.GetContextAsync();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;

    var query = HttpUtility.ParseQueryString(request.Url!.Query);
    string? result = null;
    
   if (query["response"] == "task")
    {
        User user = new();
        foreach (var item in tempUser)
        {
            if (item.UserId == int.Parse(query["userId"]))
            {
                user = item;
            }
        }
        var token = query["token"];
        var userTask = tempTask.Where(t => t.DepartmentId == int.Parse(user.DepartmentId.ToString()!)).ToList();

        

        string csvdata;
        using StringWriter writer = new StringWriter();
        using (CsvWriter csvwriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csvwriter.WriteRecords(userTask);
            csvdata = writer.ToString();
        }

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvdata);
        response.ContentLength64 = buffer.Length;
        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
        Console.WriteLine($"{request.Url} - обработан");
    }
    else if (query["response"] == "authorization")
    {
        var login = query["login"];
        var password = query["password"];
    }
    Console.WriteLine("Запрос обработан");    
}


//server.Stop();
//server.Close();