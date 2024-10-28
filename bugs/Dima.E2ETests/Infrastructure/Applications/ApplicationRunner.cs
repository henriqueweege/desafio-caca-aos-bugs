using System.Diagnostics;
using CliWrap;
using CliWrap.Buffered;

namespace Dima.E2ETests.Infrastructure.Applications;

internal class ApplicationRunner
{
    private Process process;

    internal async Task Run(string applicationPath)
    {
        await Cli
            .Wrap($"dotnet")
            .WithArguments($"run --project {applicationPath}").ExecuteAsync();
        
    }
}
