namespace Infrastructure.Data;
using Npgsql;

public static class Database
{
    private static string connection = "Host=localhost;Port=5432;Database=minishop;Username=postgres;Password=1234";
    public static NpgsqlConnection Connection()=> new NpgsqlConnection(connection);
}
