using System.Diagnostics;

namespace Dima.E2ETests.Infrastructure.Applications;

internal sealed class WebAppHandler
{
    public static string Url = "http://localhost:5028";

    private const string ProjectPath = "../../../../Dima.Web";

    private static ApplicationRunner applicationRunner = new();

    public static void RunWebApp()
    {
         applicationRunner.Run(ProjectPath);
    }
}
