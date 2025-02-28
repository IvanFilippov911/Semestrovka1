using Justwatch.Core;

namespace Justwatch.Server.Handlers;

abstract class Handler
{
    public Handler Successor { get; set; }
    public abstract void HandleRequest(HttpRequestContext context);
}
