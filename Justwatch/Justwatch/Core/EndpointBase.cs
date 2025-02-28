using Justwatch.Core.HttpResponse;

namespace Justwatch.Core;

public abstract class EndpointBase
{
    protected HttpRequestContext Context { get; private set; }

    internal void SetContext(HttpRequestContext context)
    {
        Context = context;
    }

    protected IHttpResponseResult Html(string html) => new HtmlResult(html);
    protected IHttpResponseResult Json(object data) => new JsonResult(data);

    protected IHttpResponseResult Redirect(string location)
    {
        var result = new RedirectResult(location);
        result.Context = Context;
        return result;
    }
}          