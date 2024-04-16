using System.Net;
using System.Runtime.CompilerServices;
using TaskMasterServer.Data;
using TaskMasterServer.DataBase;

namespace TaskMasterServer.Service.HTTP
{
    internal static class Logical
    {
        public static string GetRequestBody(HttpListenerRequest request)
        {
            string? requestBody = null;
            using (StreamReader reader = new StreamReader(request.InputStream, request.ContentEncoding))
            {
                requestBody = reader.ReadToEnd();
            }
            return requestBody;
        }
        public static Data.Data Authorization(UserData user)
        {
            Data.Data data = new Data.Data();
            User userBD = DataBd.ReadUser().Where(u => u.Login == user.Login).ToList()[0];
            user.GetUserDataConvertUserBD(userBD);
            if (userBD.Password == user.Password)
            {
                data.AddUser(user);
            }
            data.AddTasks(Read.ReadTasks(user));
            return data;
        }
    }
}
