using taskMasterClientTest.Data;
using taskMasterClientTest.Service;

Data data = new Data();

string postServer = "http://176.123.160.24:8080";
string postLocal = "http://localhost:8080";


Console.WriteLine("Нажмите любую клавишу для продолжения начала работы приложения...");
Console.ReadKey();

using (HttpClient client = new HttpClient())
{
    User user = new User(postLocal);
    user.Login(client, "it2", "2");
    //user.Display();
    //user.CreateTask(client);
    //user.CreateUser(client);
    //user.CreateDepartment(client);
    //user.DisplayAllData();
    user.UpdateTask(client);
}

Console.WriteLine("Нажмите любую клаавишу для завершения....");
Console.ReadKey();



