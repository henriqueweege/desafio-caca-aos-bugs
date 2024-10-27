using Dima.Core;

namespace Dima.E2ETests.Infrastructure.Applications;

internal sealed class ApiHandler
{
    private static ApplicationRunner applicationRunner = new();
    public static async Task RunApi()
    {
        await applicationRunner.Run("dotnet run --project ../../../../Dima.Api/Dima.Api.csproj");
        Environment.SetEnvironmentVariable(Configuration.E2ETestEnv, "true");
    }
    public static void DisposeApp()
    {
        applicationRunner.Dispose();
    }
}