using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business;

DataBd.UpdateTempBD();

//Server server = new Server();
//server.Start();
//server.QueryProcessing(); 

InWorkServer server = new InWorkServer(false);
server.Start();
server.InWorkProcess();

