using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business;

DataBd.UpdateTempBD();
DataBd.CheckStatusTask();

InWorkServer server = new InWorkServer(false, 8888);
server.Start();
server.InWorkProcess();

