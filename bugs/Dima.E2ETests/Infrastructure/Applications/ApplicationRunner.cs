using System.Diagnostics;
using System.Runtime.InteropServices;
using CliWrap;

namespace Dima.E2ETests.Infrastructure.Applications;

internal class ApplicationRunner
{
    private Process process;

    internal async Task Run(string applicationPath)
    {
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            ProcessStartInfo processInfo;

            processInfo = new ProcessStartInfo("cmd.exe", $"/K dotnet run --project {applicationPath}");
            processInfo.UseShellExecute = true;

            Process.Start(processInfo);
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            await Cli
            .Wrap($"dotnet")
            .WithArguments($"run --project {applicationPath}")
            .WithValidation(CommandResultValidation.None)
            .ExecuteAsync();

        }
    }
}
