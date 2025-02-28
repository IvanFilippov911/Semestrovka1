using System.Text;
using System.Text.Json;

namespace Justwatch.Core.HttpResponse;

internal class JsonResult : IHttpResponseResult
{
    private readonly object _data;
    public HttpRequestContext Context { get; set; }
    public JsonResult(object data)
    {
        _data = data;
    }

    public void Execute(HttpRequestContext context)
    {
        var json = JsonSerializer.Serialize(_data);

        byte[] buffer = Encoding.UTF8.GetBytes(json);
        context.Response.Headers.Add("Content-Type", "application/json; charset=UTF-8");
     
        context.Response.ContentLength64 = buffer.Length;
        using Stream output = context.Response.OutputStream;

        output.Write(buffer);
        output.Flush();
    }
}
