using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Dima.E2ETests.Infrastructure.Applications.Strategies;

internal class WindowsRunner : IApplicationRunner
{

    public Task Run(string applicationPath)
    {
        ProcessStartInfo processInfo;

        processInfo = new ProcessStartInfo("cmd.exe", $"/K dotnet run --project {applicationPath}");
        processInfo.UseShellExecute = true;

        Process.Start(processInfo);

        return Task.CompletedTask;
    }

    public bool AppliesToSO()
    => RuntimeInformation.IsOSPlatform(OSPlatform.Windows);

}
