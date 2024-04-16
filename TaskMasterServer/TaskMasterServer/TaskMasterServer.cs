using TaskMasterServer.DataBase;
using TaskMasterServer.Service.HTTP;

DataBd.UpdateTempBD();

Server server = new Server();
server.Start();
server.QueryProcessing(); 