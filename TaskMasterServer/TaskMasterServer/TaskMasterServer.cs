using TaskMasterServer.DataBase;
using TaskMasterServer.Service.Business;

DataBd.UpdateTempBD();

InWorkServer server = new InWorkServer(true);
server.Start();
server.InWorkProcess();

