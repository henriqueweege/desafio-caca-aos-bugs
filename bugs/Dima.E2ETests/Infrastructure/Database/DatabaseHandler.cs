using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace Dima.E2ETests.Infrastructure.Database;

public class DatabaseHandler
{
    private static readonly MsSqlContainer Container = new MsSqlBuilder()
                            .WithPassword("somePassw0rd!")
                            .WithImage("clearlife/ms-sql-express-2019-win-2022")
                            .WithPortBinding(37000, 1433)
                            .Build();

    internal static SqlConnection DatabaseConnection;

    internal static string ConnectionString;

    public static async Task StartAsync()
    {
        await Container.StartAsync();
        DatabaseConnection = new SqlConnection(Container.GetConnectionString());
        ConnectionString = BuildDatabaseConnectionStrings();
    }

    private static async Task ExecuteCommand(string sqlCommand)
    {
        await using var createCommand = DatabaseConnection.CreateCommand();
        createCommand.CommandText = sqlCommand;
        await createCommand.ExecuteNonQueryAsync();
    }

    private static string BuildDatabaseConnectionStrings()
    {
        var splitAtDatabase = Container.GetConnectionString().Split("Database");

        var server = splitAtDatabase[0];
        var auth = splitAtDatabase[1].Split("master;")[1];

        var connectionString = $"{server}Database=dimadb;{auth}";

        return connectionString;
    }
}
