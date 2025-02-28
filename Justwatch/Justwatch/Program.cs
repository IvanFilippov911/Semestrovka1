using Justwatch.Core.Config;
using Justwatch.Server;

namespace Justwatch;

internal class Program
{
    static async Task Main(string[] args)
    {
        AppConfig.GetInstance();
        var prefixes = new[] { $"http://{AppConfig.ServerSettings.Domain}:{AppConfig.ServerSettings.Port}/" };
        var server = new HttpServer(prefixes);
        await server.StartAsync();

        
    }
}