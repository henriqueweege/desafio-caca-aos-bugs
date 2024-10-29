using CliWrap;
using System.Runtime.InteropServices;

namespace Dima.E2ETests.Infrastructure.Applications.Strategies;

internal class LinuxRunner : IRunnerStrategy
{

    public async Task Run(string applicationPath)
    {
        await Cli
            .Wrap($"dotnet")
            .WithArguments($"run --project {applicationPath}")
            .WithValidation(CommandResultValidation.None)
            .ExecuteAsync();
    }

    public bool AppliesToSO()
        => RuntimeInformation.IsOSPlatform(OSPlatform.Linux);
}
