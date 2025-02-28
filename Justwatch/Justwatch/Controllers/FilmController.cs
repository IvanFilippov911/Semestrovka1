using Justwatch.Core.Attributes;
using Justwatch.Core.HttpResponse;
using Justwatch.Core;
using Justwatch.Core.Config;
using Justwatch.Services;



namespace Justwatch.Controllers;

internal class FilmController : EndpointBase
{
    private readonly TemplateRenderingService _templateRenderingService;

    public FilmController()
    {
        _templateRenderingService = new TemplateRenderingService(AppConfig.ConnectionString);
    }

    [Get("film")]
    public IHttpResponseResult GetFilmPage(int FilmId)
    {
        var template = File.ReadAllText(Path.Combine(AppConfig.ServerSettings.StaticDirectoryPath, "film-page.html"));
        var filmPageContent = _templateRenderingService.RenderFilmPage(FilmId, template);
        return Html(filmPageContent);
    }
}
