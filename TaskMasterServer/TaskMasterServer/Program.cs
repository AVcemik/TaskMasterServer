using System.Net;
using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Text;

string responsetText =
    @"<!DOCTYPE html>
        <html>
            <head>
                <meta charset='utf8'>
                <titel>тестовый заголовок</titel>
            </head>
            <body>
                <h2>Тестовый ответ</h2>
            </body>
        </html>";
byte[] buffer = Encoding.UTF8.GetBytes(responsetText);


string local = "http://127.0.0.1:8888/connection/";
HttpListener server = new HttpListener();

server.Prefixes.Add(local);

server.Start();

var context = await server.GetContextAsync();

var request = context.Request;
var response = context.Response;
var user = context.User;

response.ContentLength64 = buffer.Length;
using Stream output = response.OutputStream;

await output.WriteAsync(buffer);
await output.FlushAsync();

Console.WriteLine("запрос обработан");



server.Stop();
server.Close();