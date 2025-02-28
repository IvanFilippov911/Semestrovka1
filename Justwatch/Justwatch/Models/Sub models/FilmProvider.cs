

namespace Justwatch.Models.Sub_models;

public class FilmProvider
{
    public int film_id { get; set; }
    public Film film { get; set; }

    public int provider_id { get; set; }
    public Provider provider { get; set; }

    public int duration { get; set; }

    public int price { get; set; }
}