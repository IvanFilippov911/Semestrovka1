
using System.Text;

namespace Justwatch.Core.HttpResponse;

internal class HtmlResult : IHttpResponseResult
{
    private readonly string _htmlContent;
    public HttpRequestContext Context { get; set; }
    public HtmlResult(string htmlContent)
    {
        _htmlContent = htmlContent;
    }

    public void Execute(HttpRequestContext context)
    {
        byte[] buffer = Encoding.UTF8.GetBytes(_htmlContent);
        
        context.Response.ContentType = "text/html; charset=UTF-8";
        context.Response.ContentLength64 = buffer.Length;
        using Stream output = context.Response.OutputStream;

        output.Write(buffer);
        output.Flush();
    }
}
 