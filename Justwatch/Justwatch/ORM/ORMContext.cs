using System.Data;
using Npgsql;

namespace Justwatch.ORM;

public class ORMContext<T>
{
    private readonly string connectionString;

    public ORMContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    public T Create<T>(T entity, string tableName) where T : class
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            var sql = GenerateInsertQuery(tableName);

            using (var command = new NpgsqlCommand(sql, connection))
            {
                AddParameters(command, entity);
                command.ExecuteNonQuery();
            }
        }
        return entity;
    }

    public T ReadById<T>(int? id, string nameId, string tableName) where T : class, new()
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sql = $"SELECT * FROM \"{tableName}\" WHERE \"{nameId}\" = @{nameId}";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue($"@{nameId}", id);

                using (var reader = command.ExecuteReader())
                {
                    if (reader.Read()) return Map<T>(reader);
                }
            }
        }
        return null;
    }

    public List<T> ReadAll<T>(string tableName) where T : class, new()
    {
        var listResults = new List<T>();
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            var sql = $"SELECT * FROM \"{tableName}\"";

            using (var command = new NpgsqlCommand(sql, connection))
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        var instance = Map<T>(reader);
                        listResults.Add(instance);
                    }
                }
            }
        }
        return listResults;
    }

    public void Update<T>(int id, T entity, string tableName, string nameId)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            var sql = GenerateUpdateQuery(tableName, nameId);

            using (var command = new NpgsqlCommand(sql, connection))
            {
                AddParameters(command, entity);
                command.Parameters.AddWithValue($"@{nameId}", id);
                command.ExecuteNonQuery();
            }
        }
    }

    public void Delete(int id, string nameId, string tableName)
    {
        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();
            string sql = $"DELETE FROM \"{tableName}\" WHERE \"{nameId}\" = @{nameId}";
            using (var command = new NpgsqlCommand(sql, connection))
            {
                command.Parameters.AddWithValue("@id", id);
                command.ExecuteNonQuery();
            }
        }
    }

    private void AddParameters<T>(NpgsqlCommand command, T entity)
    {
        var properties = typeof(T).GetProperties();
        foreach (var property in properties)
        {
            var value = property.GetValue(entity) ?? DBNull.Value;
            command.Parameters.AddWithValue($"@{property.Name}", value);
        }
    }

    private string GenerateInsertQuery(string tableName)
    {
        var properties = typeof(T).GetProperties();
        if (properties == null) throw new Exception();

        var columns = string.Join(", ", properties.Where(p => p.Name.ToLower() != "id").Select(p => $"{p.Name}"));
        var values = string.Join(", ", properties.Where(p => p.Name.ToLower() != "id").Select(p => $"@{p.Name}"));
        return $"INSERT INTO \"{tableName}\" ({columns}) VALUES ({values})";
    }

    private string GenerateUpdateQuery(string tableName, string nameId)
    {
        var properties = typeof(T).GetProperties();
        var setClauses = string.Join(", ", properties
            .Where(p => p.Name != "Id")
            .Select(p => $"{p.Name} = @{p.Name}"));
        return $"UPDATE \"{tableName}\" SET {setClauses} WHERE \"{nameId} = @{nameId}";
    }

    private T Map<T>(IDataReader reader) where T : class, new()
    {
        var entity = new T();
        var properties = typeof(T).GetProperties();

        foreach (var property in properties)
        {

            try
            {
                if (reader[property.Name] != DBNull.Value)
                {
                    property.SetValue(entity, reader[property.Name]);
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                Console.WriteLine($"Error mapping field {property.Name}: {ex.Message}");
            }
        }

        return entity;
    }

    public object Where(Func<object, bool> value)
    {
        throw new NotImplementedException();
    }

    public List<int> ExecuteQuery(string sql, object parameters = null)
    {
        var results = new List<int>();

        using (var connection = new NpgsqlConnection(connectionString))
        {
            connection.Open();

            using (var command = new NpgsqlCommand(sql, connection))
            {
                if (parameters != null)
                {
                    var paramProperties = parameters.GetType().GetProperties();
                    foreach (var property in paramProperties)
                    {
                        command.Parameters.AddWithValue($"@{property.Name}", property.GetValue(parameters) ?? DBNull.Value);
                    }
                }

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (reader[0] != DBNull.Value)
                        {
                            results.Add((int)reader[0]);
                        }
                    }
                }
            }
        }

        return results;
    }

}
