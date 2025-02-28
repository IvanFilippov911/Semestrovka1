using Justwatch.Core;
using Justwatch.Core.Config;
using System.Net;
using System.Net.Mime;

namespace Justwatch.Server.Handlers;

internal class StaticFileHandler : Handler
{
    public override void HandleRequest(HttpRequestContext context)
    {
        var request = context.Request;
        bool IsGet = request.HttpMethod.Equals("GET", StringComparison.InvariantCultureIgnoreCase);
        string[] arr = request.Url.AbsolutePath.Split(".");
        bool IsFile = arr.Length >= 2;


        if (IsGet && IsFile)
        {
            try
            {
                var filePath = Path.GetFullPath(Path.Combine(AppConfig.ServerSettings.StaticDirectoryPath, request.Url.AbsolutePath.TrimStart('/')));

                var filePathError = Path.Combine(AppConfig.ServerSettings.StaticDirectoryPath, "404.html");

                if (!File.Exists(filePath))
                {

                    Console.WriteLine(filePath);
                    context.Response.StatusCode = (int)HttpStatusCode.NotFound;

                    if (!File.Exists(filePathError))
                    {
                        HttpRequestContext.SendTextResponse(context, "Page Not Found");
                        return;

                    }
                    else filePath = filePathError;
                }

                SendContentResponse(context.Response, filePath);

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred: {ex.Message}");
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                HttpRequestContext.SendTextResponse(context, "Internal Server Error");
            }
        }

        else if (Successor != null)
        {
            Successor.HandleRequest(context);
        }
    }
    private void SendContentResponse(HttpListenerResponse response, string filePath)
    {
        // определение типа контента
        response.ContentType = GetContentType(filePath);

        // Формирование ответа отправляемый в ответ код html возвращает
        byte[] buffer = File.ReadAllBytes(filePath);

        response.ContentLength64 = buffer.Length;
        response.OutputStream.Write(buffer, 0, buffer.Length);

        // Закрываем ответ
        response.OutputStream.Close();
    }

    private string GetContentType(string filePath)
    {
        var extension = filePath.Split('.').Last();
        return _contentTypes.GetValueOrDefault(extension, "text/txt");
    }

    private Dictionary<string, string> _contentTypes = new Dictionary<string, string>()
    {
        {"css","text/css"},
        {"html","text/html"},
        {"jpg","image/jpg"},
        {"jpeg","image/jpeg"},
        {"js","text/javascript"},
        {"json","application/json"},
        {"png","image/png"},
        {"svg","image/svg+xml"},
        {"ttf","font/ttf"},
        {"webp","image/webp"},
    };


}

