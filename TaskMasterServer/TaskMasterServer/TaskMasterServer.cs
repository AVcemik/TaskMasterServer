using CsvHelper;
using CsvHelper.Configuration;
using System.Globalization;
using System.Net;
using TaskMasterServer.DataBase;
using TaskBD = TaskMasterServer.DataBase.Task;


List<TaskBD> tempBD = new();

using (TaskUserDbContext dbContext = new TaskUserDbContext())
{
    tempBD = dbContext.Tasks.ToList();
}

string local = "http://127.0.0.1:8080/GetTask/";
string C1NB8 = "http://192.168.10.106:8080/1/";

HttpListener server = new HttpListener();
//server.Prefixes.Add(C1NB8);
server.Prefixes.Add($"http://*:{8080}/");
server.Start();
Console.WriteLine("Сервер запущен");

while (true)
{
    HttpListenerContext context = await server.GetContextAsync();
    HttpListenerResponse response = context.Response;

    string csvData;
    using StringWriter writer = new StringWriter();
    using (CsvWriter csvWriter = new CsvWriter(writer, new CsvConfiguration(CultureInfo.InvariantCulture)))
    {
        csvWriter.WriteRecords(tempBD);
        csvData = writer.ToString();
    }

    byte[] buffer = System.Text.Encoding.UTF8.GetBytes(csvData);
    response.ContentLength64 = buffer.Length;
    Stream output = response.OutputStream;
    output.Write(buffer, 0, buffer.Length);
    output.Close();
}


//using (TaskUserDbContext contex = new TaskUserDbContext())
//{
//    tempBD = contex.Tasks.ToList();
//}

//var csvContext = new CsvContext(new CsvConfiguration(CultureInfo.InvariantCulture));


//var request = context.Request;
//var user = context.User;

//byte[] buffer = Encoding.UTF8.GetBytes();
//response.ContentLength64 = buffer.Length;
//using Stream output = response.OutputStream;

//await output.WriteAsync(buffer);
//await output.FlushAsync();

//Console.WriteLine("Запрос обработан");

//server.Stop();
//server.Close();