using Justwatch.Server.Handlers;
using Justwatch.Core;
using System.Net;

namespace Justwatch.Server
{
    internal sealed class HttpServer
    {

        private readonly HttpListener listener;

        private readonly List<Handler> handlers = new();


        public HttpServer(string[] prefixes)
        {
            listener = new HttpListener();
            foreach (string prefix in prefixes)
            {
                Console.WriteLine($"Prefixe: {prefix}");
                listener.Prefixes.Add(prefix);
            }

            handlers.Add(new StaticFileHandler());
            handlers.Add(new EndpointsHandler());
            handlers.Add(new NotFoundHandler());
        }

        private void ConfigureHandlers()
        {
            for (int i = 0; i < handlers.Count - 1; i++)
                handlers[i].Successor = handlers[i + 1];
        }

        public async Task StartAsync()
        {
            ConfigureHandlers();
            listener.Start();
            Console.WriteLine("Сервер запущен и ожидает запросов...");

            while (listener.IsListening)
            {
                var context = await listener.GetContextAsync();
                var requestContext = new HttpRequestContext(context.Request, context.Response);

                await ProcessRequest(requestContext);
            }
        }

        private async Task ProcessRequest(HttpRequestContext context)
        {
            if (handlers.Count > 0) handlers[0].HandleRequest(context);
        }

        public void Stop()
        {
            listener.Stop();
            Console.WriteLine("Сервер остановлен.");
        }
    }
}
