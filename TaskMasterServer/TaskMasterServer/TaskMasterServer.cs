using TaskMasterServer.DataBase;
using TaskMasterServer.Service;


DataBd.UpdateTempBD();
DataBd.CheckStatusTask();

InWorkServer server = new (true, 8080);
server.Start();
server.InWorkProcess();