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
server.Start();
Console.WriteLine("Сервер запущен");

while (true)
{
    HttpListenerContext context = await server.GetContextAsync();
    HttpListenerRequest request = context.Request;
    HttpListenerResponse response = context.Response;

    var header = HttpUtility.ParseQueryString(request.Headers.ToString()!);
    var query = HttpUtility.ParseQueryString(request.Url!.Query);
    string? result = null;
    
    if (header.ToString() == "task")
    {
        User user = new();
        var userId = query["userId"];
        foreach (var item in tempUser)
        {
            if (item.UserId == int.Parse(userId))
            {
                user = item;
            }
        }
        //var department = tempUser.Where(u => u.Department == int.Parse(tempDepartment));
        var token = query["token"];
        var userTask = tempTask.Where(t => t.DepartmentId == int.Parse(user.DepartmentId.ToString()!));

        string csvdata;
        using StringWriter writer = new StringWriter();
        using (CsvWriter csvwriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
        {
            csvwriter.WriteRecord(result);
            csvdata = writer.ToString();
        }

        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvdata);
        response.ContentLength64 = buffer.Length;
        Stream output = response.OutputStream;
        output.Write(buffer, 0, buffer.Length);
        output.Close();
        Console.WriteLine($"{request.Url} - обработан");
    }
    else if (header.ToString() == "authorization")
    {
        var login = query["login"];
        var password = query["password"];
    }
    Console.WriteLine("Запрос обработан");


    //var usertask = tempUser.Where(t => t.userid == int.parse(userid!)).tolist();

    
}


//server.Stop();
//server.Close();