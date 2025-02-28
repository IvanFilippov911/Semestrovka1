using Justwatch.Core;
using System.Net;


namespace Justwatch.Server.Handlers;

internal class NotFoundHandler : Handler
{
    public override void HandleRequest(HttpRequestContext context)
    {
        context.Response.StatusCode = (int)HttpStatusCode.NotFound;

        HttpRequestContext.SendTextResponse(context, "404 - Page Not Found");
    }
}
