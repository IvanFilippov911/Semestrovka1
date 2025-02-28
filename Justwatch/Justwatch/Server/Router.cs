using Justwatch.Core;
using Justwatch.Core.Attributes;
using System.Reflection;

namespace Justwatch.Server;

internal sealed class Router
{
    private readonly Dictionary<string, List<(HttpMethod method, MethodInfo methodInfo, Type endpointType)>> routes = new();

    public void RegisterEndpointsFromAssemblies(Assembly[] assemblies)
    {
        foreach (Assembly assembly in assemblies)
        {
            var endpointTypes = assembly.GetTypes()
                .Where(t => typeof(EndpointBase).IsAssignableFrom(t) && !t.IsAbstract);

            foreach (var endpointType in endpointTypes)
            {
                var methods = endpointType.GetMethods();

                foreach (var methodInfo in methods)
                {
                    RegisterRoutesFromAttributes(methodInfo, endpointType);
                }
            }
        }
    }

    public (HttpMethod method, MethodInfo methodInfo, Type endpointType)? FindRoute(string url, string httpMethod)
    {
        Console.WriteLine($"URL:<{url}>    httpMethod:<{httpMethod}>");
        if (routes.ContainsKey(url))
        {
            return routes[url]
                .FirstOrDefault(r => r.method.ToString().Equals(httpMethod, StringComparison.OrdinalIgnoreCase));
        }

        return null;
    }

    private void RegisterRoute(string route, HttpMethod method, MethodInfo methodInfo, Type endpointType)
    {
        if (!routes.ContainsKey(route))
        {
            routes[route] = new();
        }

        if (routes[route].Any(r => r.method == method))
        {
            Console.WriteLine($"ERROR: Duplicate route '{route}' for HTTP method '{method}'");
            Environment.Exit(1);
        }

        routes[route].Add((method, methodInfo, endpointType));
    }

    private void RegisterRoutesFromAttributes(MethodInfo methodInfo, Type endpointType)
    {
        var httpAttributes = new Dictionary<Type, HttpMethod>
        {
            { typeof(GetAttribute), HttpMethod.Get },
            { typeof(PostAttribute), HttpMethod.Post }
        };

        foreach (var attributePair in httpAttributes)
        {
            var attribute = methodInfo.GetCustomAttribute(attributePair.Key);
            if (attribute != null)
            {
                var routeProperty = attribute.GetType().GetProperty("Route");
                if (routeProperty != null)
                {
                    var route = routeProperty.GetValue(attribute)?.ToString();
                    if (!string.IsNullOrEmpty(route))
                    {
                        RegisterRoute(route, attributePair.Value, methodInfo, endpointType);
                    }
                }
            }
        }
    }
}
