using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business;

DataBd.UpdateTempBD();

InWorkServer server = new InWorkServer(false);
server.Start();
server.InWorkProcess();

