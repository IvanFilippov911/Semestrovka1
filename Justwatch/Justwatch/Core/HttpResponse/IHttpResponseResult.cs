using System.Net;

namespace Justwatch.Core.HttpResponse;

public interface IHttpResponseResult
{
    void Execute(HttpRequestContext context);
    HttpRequestContext Context { get; set; }
}

public static class HttpResponseResultExtensions
{
    public static IHttpResponseResult WithCookie(this IHttpResponseResult result, string name, string value, int expiresDays = 30)
    {
        if (result.Context.Response.Cookies == null)
        {
            Console.WriteLine("Cookies collection is null. Initializing...");
            result.Context.Response.Cookies = new CookieCollection();
        }

        var cookie = new Cookie(name, value)
        {
            Expires = DateTime.UtcNow.AddDays(expiresDays)
        };

        result.Context.Response.Cookies.Add(cookie);
        return result;
    }
}
