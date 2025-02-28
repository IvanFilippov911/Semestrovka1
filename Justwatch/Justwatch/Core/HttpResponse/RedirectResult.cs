
using System.Net;

namespace Justwatch.Core.HttpResponse;

public class RedirectResult : IHttpResponseResult
{
    private readonly string _location;
    public HttpRequestContext Context { get; set; }

    public RedirectResult(string location)
    {
        _location = location;
    }

    public void Execute(HttpRequestContext context)
    {
        Context = context;
        var response = Context.Response;
        response.StatusCode = 302;
        response.Headers["Location"] = _location;

        foreach (Cookie cookie in response.Cookies)
        {
            var cookieHeader = $"{cookie.Name}={cookie.Value}; Expires={cookie.Expires:R}; Path=/;";
            response.Headers.Add("Set-Cookie", cookieHeader);
            Console.WriteLine($"Set-Cookie: {cookieHeader}");
        }

        using (var writer = new StreamWriter(response.OutputStream))
        {
            writer.Write("");
        }

        Console.WriteLine($"Redirecting to: {_location}");
    }
}
