using System.Net;

namespace TaskMasterServer.Service.HTTP
{
    internal interface IResponseSend
    {
        public void Send(string csvData, HttpListenerResponse response)
        {
            byte[] buffer = System.Text.Encoding.Unicode.GetBytes(csvData);
            response.ContentLength64 = buffer.Length;
            Stream output = response.OutputStream;
            output.Write(buffer, 0, buffer.Length);

            output.Close();
        }
    }
}
