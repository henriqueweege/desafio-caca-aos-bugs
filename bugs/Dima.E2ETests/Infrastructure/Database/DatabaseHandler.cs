using Microsoft.Data.SqlClient;
using Testcontainers.MsSql;

namespace Dima.E2ETests.Infrastructure.Database;

public class DatabaseHandler
{
    private static readonly MsSqlContainer Container = new MsSqlBuilder()
                            .WithPassword("somePassw0rd!")
                            .WithPortBinding(37000, 1433)
                            .Build();

    public static async Task StartAsync()
    {
        await Container.StartAsync();
    }
}
