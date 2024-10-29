using System.Diagnostics;
using System.Runtime.InteropServices;
using CliWrap;
using Dima.E2ETests.Infrastructure.Applications.Strategies;

namespace Dima.E2ETests.Infrastructure.Applications;

internal class ApplicationRunner
{
    private IEnumerable<IRunnerStrategy> _runners = new List<IRunnerStrategy>()
    {
        new LinuxRunner(), new WindowsRunner()
    };

    internal async Task Run(string applicationPath)
    {
        var runner = _runners.FirstOrDefault(x=>x.AppliesToSO());

        if (runner == null) 
        {
            throw new EntryPointNotFoundException("E2E test project just supports Windows and Linux.");
        }

        await runner.Run(applicationPath);
    }
}
