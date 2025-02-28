using System.Net;
using System.Text;

namespace Justwatch.Core;

public sealed class HttpRequestContext
{
    public HttpListenerRequest Request { get; }

    public HttpListenerResponse Response { get; }

    public HttpRequestContext(HttpListenerRequest request, HttpListenerResponse response)
    {
        Request = request ?? throw new ArgumentNullException(nameof(request));
        Response = response ?? throw new ArgumentNullException(nameof(response));
    }

    public static async Task SendFileResponse(HttpRequestContext context, string filePath)
    {
        byte[] responseFile = await File.ReadAllBytesAsync(filePath);
        context.Response.ContentType = GetContentType(Path.GetExtension(filePath));
        context.Response.ContentLength64 = responseFile.Length;

        await context.Response.OutputStream.WriteAsync(responseFile, 0, responseFile.Length);
        context.Response.OutputStream.Close();
    }

    public static void SendTextResponse(HttpRequestContext context, string text)
    {
        byte[] textBytes = Encoding.UTF8.GetBytes(text);
        context.Response.ContentType = "text/plain";
        context.Response.ContentLength64 = textBytes.Length;

        context.Response.OutputStream.Write(textBytes, 0, textBytes.Length);
        context.Response.OutputStream.Close();
    }

    private static string GetContentType(string? extension)
    {
        if (extension == null)
        {
            throw new ArgumentNullException(nameof(extension), "Extension cannot be null.");
        }

        return extension.ToLower() switch
        {
            ".html" => "text/html",
            ".css" => "text/css",
            ".js" => "application/javascript",
            ".png" => "image/png",
            ".jpg" => "image/jpeg",
            ".jpeg" => "image/jpeg",
            ".gif" => "image/gif",
            ".ico" => "image/x-icon",
            ".json" => "application/json",
            ".xml" => "application/xml",
            _ => "application/octet-stream"
        };
    }
}
