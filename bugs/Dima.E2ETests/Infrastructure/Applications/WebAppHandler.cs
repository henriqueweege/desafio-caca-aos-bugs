using System.Diagnostics;

namespace Dima.E2ETests.Infrastructure.Applications;

internal sealed class WebAppHandler
{
    public static string Url = "http://localhost:5028";

    private static ApplicationRunner applicationRunner = new();

    public static void RunWebApp()
    {
        applicationRunner.Run("dotnet run --project ../../../../Dima.Web/Dima.Web.csproj");
    }

    public static void DisposeApp()
    {
        applicationRunner.Dispose();
    }
}
