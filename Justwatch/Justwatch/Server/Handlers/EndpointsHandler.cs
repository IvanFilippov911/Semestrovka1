using Justwatch.Core.HttpResponse;
using System.Reflection;
using System.Web;
using Justwatch.Core;
using System.Xml;
using Justwatch.ModelsDto;

namespace Justwatch.Server.Handlers;

internal class EndpointsHandler : Handler
{
    private readonly Router router;

    public EndpointsHandler()
    {
        router = new Router();
        router.RegisterEndpointsFromAssemblies(new[] { Assembly.GetExecutingAssembly() });
    }

    public override void HandleRequest(HttpRequestContext context)
    {
        var request = context.Request;
        var url = request.Url.LocalPath.Trim('/');
        var requestMethod = request.HttpMethod;
        var route = router.FindRoute(url, requestMethod);
  
        
        if (route != null)
        {
            var (method, methodInfo, endpointType) = route.Value;
            if (endpointType == null)
                Console.WriteLine($"Endpoint type is null for route URL: {url} and Method: {requestMethod}");
            
            var endpointInstance = Activator.CreateInstance(endpointType) as EndpointBase;
            if (endpointInstance != null)
            {
                endpointInstance.SetContext(context);
                var parameters = GetMethodParameters(methodInfo, context);
                var result = methodInfo.Invoke(endpointInstance, parameters) as IHttpResponseResult;
                result?.Execute(context);
            }
        }
        else if (Successor != null)
        {
            Successor.HandleRequest(context);
        }
    }

    private object[] GetMethodParameters(MethodInfo methodInfo, HttpRequestContext context)
    {
        var parameters = methodInfo.GetParameters();
        var values = new object[parameters.Length];

        if (context.Request.HttpMethod.Equals("GET", StringComparison.InvariantCultureIgnoreCase))
        {

            var queryParameters = HttpUtility.ParseQueryString(context.Request.Url.Query);
            for (int i = 0; i < parameters.Length; i++)
            {
                var paramName = parameters[i].Name;
                var paramType = parameters[i].ParameterType;
                var value = queryParameters[paramName];
                values[i] = ConvertValue(value, paramType);
            }
        }
        else if (context.Request.HttpMethod.Equals("POST", StringComparison.InvariantCultureIgnoreCase))
        {
            using var reader = new StreamReader(context.Request.InputStream);
            var body = reader.ReadToEnd();

            if (context.Request.ContentType == "application/x-www-form-urlencoded")
            {
                var formParameters = HttpUtility.ParseQueryString(body);
                for (int i = 0; i < parameters.Length; i++)
                {
                    var paramName = parameters[i].Name;
                    var paramType = parameters[i].ParameterType;
                    var value = formParameters[paramName];
                    values[i] = ConvertValue(value, paramType);
                }
            }
            else if (context.Request.ContentType == "application/json")
            {
                Console.WriteLine(body);
                if (string.IsNullOrEmpty(body))
                {
                    Console.WriteLine("Тело запроса пусто!");
                    return values;  
                }

                var jsonObject = System.Text.Json.JsonSerializer.Deserialize(body, methodInfo.GetParameters()[0].ParameterType);
                values[0] = jsonObject;

                Console.WriteLine("Десериализованный объект:");
                Console.WriteLine(System.Text.Json.JsonSerializer.Serialize(jsonObject, new System.Text.Json.JsonSerializerOptions { WriteIndented = true }));
            }
        }

        return values;
    }

    private object ConvertValue(string value, Type targetType)
    {
        if (value == null)
        {
            return targetType.IsValueType ? Activator.CreateInstance(targetType) : null;
        }

        return Convert.ChangeType(value, targetType);
    }
}

