using taskMasterClientTest.Data;
using taskMasterClientTest.Service;

Data data = new Data();


string postServer = "http://176.123.160.24";
string localServer = "http://localhost";
int portCifra = 8080;
int portHome = 8888;
string connect = $"{postServer}:{portCifra}";




Console.WriteLine("Нажмите любую клавишу для продолжения начала работы приложения...");
Console.ReadKey();

using (HttpClient client = new HttpClient())
{
    User user = new User(connect);
    user.Login(client, "admin", "admin");
    //user.Display();
    user.DisplayAllData();
    //user.CreateTask(client);
    //user.CreateUser(client);
    //user.CreateDepartment(client);
    //user.UpdateTask(client);
    //user.DeleteTask(client);
}

Console.WriteLine("Нажмите любую клаавишу для завершения....");
Console.ReadKey();



