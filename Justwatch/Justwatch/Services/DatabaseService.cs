using Justwatch.Models;
using Justwatch.Models.Sub_models;
using Justwatch.ORM;
using System.Xml;

namespace Justwatch.Services;

public class DatabaseService
{
    private readonly ORMContext<object> _ormContext;

    public DatabaseService(string connectionString)
    {
        _ormContext = new ORMContext<object>(connectionString);
    }

    public Dictionary<string, object> GetFilm(int filmId)
    {
        var data = new Dictionary<string, object>();

        var film = _ormContext.ReadById<Film>(filmId, "film_id", "films");
        if (film != null)
        {
            foreach (var prop in typeof(Film).GetProperties())
            {
                string propertyName = $"Film.{prop.Name}";
                data[propertyName] = prop.GetValue(film);
            }

            var actors = GetRelatedEntities<Actor>(filmId, "film_actor", "actor_id", "actor_id", "actors");
            var providers = GetRelatedEntities<Provider>(filmId, "film_provider", "provider_id", "provider_id", "providers");
            data["Actor"] = actors;
            data["Provider"] = providers;
        }

        return data;
    }

    private List<T> GetRelatedEntities<T>(int filmId, string joinTable, string foreignKeyColumn, string primaryKeyColumn, string relatedTable) where T : class, new()
    {

        var relatedEntityIds = _ormContext.ExecuteQuery(
         $"SELECT r.{primaryKeyColumn} " +
         $"FROM {relatedTable} r " +
         $"JOIN {joinTable} j ON r.{primaryKeyColumn} = j.{foreignKeyColumn} " +
         $"WHERE j.film_id = @filmId " +
         $"LIMIT 5", new { filmId });

        var relatedEntities = new List<T>();
        Console.WriteLine($"Related Entity IDs: {string.Join(", ", relatedEntityIds)}");

        foreach (var entityId in relatedEntityIds)
        {

            var entity = _ormContext.ReadById<T>(entityId, primaryKeyColumn, relatedTable);
            if (entity != null)
            {
                relatedEntities.Add(entity);
            }
        }

        return relatedEntities;
    }


    public List<Film> GetFilms()
    {
        var films = _ormContext.ReadAll<Film>("films");
        return films;
    }
}
