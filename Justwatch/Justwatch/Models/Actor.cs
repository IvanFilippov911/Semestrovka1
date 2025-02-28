

using Justwatch.Models.Sub_models;
using System.ComponentModel.DataAnnotations.Schema;

namespace Justwatch.Models;

public class Actor
{
    public int actor_id { get; set; }

    public string full_name { get; set; }

    public DateTime date_of_birth { get; set; }

    public List<FilmActor> film_actors { get; set; } = new();
}
