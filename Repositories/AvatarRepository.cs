using Microsoft.Data.Sqlite;

namespace UserIdentifierService.Repositories;

public class AvatarRepository
{
    private readonly string _connectionString;

    public AvatarRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public string GetImageUrlById(int id)
    {
        using var connection = new SqliteConnection(_connectionString);
        connection.Open();

        const string query = "SELECT url FROM images WHERE id = @id";
        using var command = new SqliteCommand(query, connection);
        command.Parameters.AddWithValue("@id", id);

        var result = command.ExecuteScalar();
        return result?.ToString();
    }
}