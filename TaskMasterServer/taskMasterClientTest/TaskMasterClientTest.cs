using taskMasterClientTest.Data;
using taskMasterClientTest.Service;

Data data = new Data();
int port = 8888;
string postServer = "http://176.123.160.24:8080";
string postLocal = $"http://localhost:{port}";


Console.WriteLine("Нажмите любую клавишу для продолжения начала работы приложения...");
Console.ReadKey();

using (HttpClient client = new HttpClient())
{
    User user = new User(postLocal);
    user.Login(client, "admin", "admin");
    //user.Display();
    user.DisplayAllData();
    //user.CreateTask(client);
    //user.CreateUser(client);
    //user.CreateDepartment(client);
    //user.UpdateTask(client);
    user.DeleteTask(client);
}

Console.WriteLine("Нажмите любую клаавишу для завершения....");
Console.ReadKey();



