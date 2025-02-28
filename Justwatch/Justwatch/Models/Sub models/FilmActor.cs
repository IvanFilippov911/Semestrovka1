

namespace Justwatch.Models.Sub_models;

public class FilmActor
{
    public int film_id { get; set; }
    public Film film { get; set; }

    public int actor_id { get; set; }
    public Actor actor { get; set; }

    public string role { get; set; }
}