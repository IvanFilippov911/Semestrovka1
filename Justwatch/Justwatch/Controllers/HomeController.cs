using Justwatch.Core.Attributes;
using Justwatch.Core.Config;
using Justwatch.Core.HttpResponse;
using Justwatch.Core;
using Justwatch.Services;

namespace Justwatch.Controllers;

internal class HomeController : EndpointBase
{
    private readonly TemplateRenderingService _templateRenderingService;
    private readonly AuthHelper _authHelper;

    public HomeController()
    {
        _authHelper = new AuthHelper();
        _templateRenderingService = new TemplateRenderingService(AppConfig.ConnectionString);
    }

    [Get("home")]
    public IHttpResponseResult GetHomePage()
    {
        var isAuthorized = _authHelper.IsAuthorized(Context);
        var template = File.ReadAllText(Path.Combine(AppConfig.ServerSettings.StaticDirectoryPath, "main-page.html"));
        var homePageContent = _templateRenderingService.RenderHomePage(template, isAuthorized);

        return Html(homePageContent);
    }
}
