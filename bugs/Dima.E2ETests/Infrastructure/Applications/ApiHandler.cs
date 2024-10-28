using Dima.Core;

namespace Dima.E2ETests.Infrastructure.Applications;

internal sealed class ApiHandler
{
    private static ApplicationRunner applicationRunner = new();
    private const string ProjectPath = "../../../../Dima.Api/Dima.Api.csproj";

    public static void RunApi()
    {
        Environment.SetEnvironmentVariable(Configuration.E2ETestEnv, "true");
        applicationRunner.Run(ProjectPath).GetAwaiter().GetResult();
    }
}