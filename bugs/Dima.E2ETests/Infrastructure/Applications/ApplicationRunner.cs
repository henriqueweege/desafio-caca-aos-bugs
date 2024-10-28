using System.Diagnostics;

namespace Dima.E2ETests.Infrastructure.Applications;

internal class ApplicationRunner
{
    private Process process;

    internal void Run(string applicationPath)
    {
        ProcessStartInfo processInfo;

        processInfo = new ProcessStartInfo("cmd.exe", $"/K {applicationPath}");
        processInfo.UseShellExecute = true;
        
        process = Process.Start(processInfo);

    }

    public void Dispose()
    {

        //Process.GetProcessesByName(process.ProcessName).First().Kill();
        //Process.GetProcessesByName(process.ProcessName).First().Close();
        //process.Dispose();
    }
}
