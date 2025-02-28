using Justwatch.Models;
using Justwatch.Templates;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Justwatch.Services;

public class TemplateRenderingService
{
    private readonly DatabaseService _dbService;
    private readonly TemplateGenerator _templateGenerator;

    public TemplateRenderingService(string connectionString)
    {
        _dbService = new DatabaseService(connectionString);
        _templateGenerator = new TemplateGenerator();
    }

    public string RenderFilmPage(int filmId, string template)
    {
        var filmData = _dbService.GetFilm(filmId);
        foreach (var kvp in filmData)
        {
            Console.WriteLine($"Key: {kvp.Key}, Value: {kvp.Value}");
        }

        return _templateGenerator.Render(template, filmData);
    }

    public string RenderHomePage(string template, bool isAuthorized)
    {
        var films = _dbService.GetFilms();
        var filmsData = new Dictionary<string, object> { { "films", films } };
        if (isAuthorized)
        {
            filmsData["auth"] = "<button class=\"favorite-button\">❤️</button>";
            filmsData["enter"] = "<p style=\"color:white\"> Вы авторизованы </p>";
        }
        else
        {
            filmsData["auth"] = "";
            filmsData["enter"] = "<a class=\"nav__button-link\" href=\"#\" id=\"openModalBtn\">Войти</a>";
        }
        return _templateGenerator.Render(template, filmsData);
    }
}
