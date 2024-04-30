using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business;

DataBd.UpdateTempBD();
DataBd.CheckStatusTask();

InWorkServer server = new InWorkServer(true, 8080);
server.Start();
server.InWorkProcess();