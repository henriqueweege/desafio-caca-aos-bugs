using BlazeWright;
using BlazeWright.NUnit;
using Microsoft.Playwright;
using System.Diagnostics;

namespace Dima.InterfaceTest
{
    [Parallelizable(ParallelScope.Self)]
    [TestFixture]
    internal class Tests 
    {
        [Test]
        public async Task Test1()
        {
            var proc1 = new ProcessStartInfo();
            proc1.UseShellExecute = true;
            proc1.WorkingDirectory = Directory.GetCurrentDirectory();
            proc1.Arguments = "Dima.Web.dll";
            proc1.FileName = "dotnet.exe";
            Process.Start(proc1);
            // try
            // {
            //     var dllFile = new FileInfo(@"./Dima.Web.dll");
            //
            //     System.Reflection.Assembly dll1 = System.Reflection.Assembly.LoadFile(dllFile.FullName);
            //     if (dll1 != null)
            //     {
            //         object obj = dll1.CreateInstance("Program");
            //         if (obj != null)
            //         {
            //             System.Reflection.MethodInfo mi = obj.GetType().GetMethod("Main");
            //             mi.Invoke(obj, new object [0]);
            //         }
            //     }
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            // try
            // {
            //     DllHelper.function1();
            // }
            // catch (Exception ex)
            // {
            //     Console.WriteLine(ex.Message);
            // }
            
            // string command = "cd ..\n"; 
            // string argss = " "; 
            // string verb = " "; 
            //  
            // ProcessStartInfo procInfo = new ProcessStartInfo(); 
            // procInfo.WindowStyle = ProcessWindowStyle.Normal; 
            // procInfo.UseShellExecute = false; 
            // procInfo.FileName = command;   // 'sh' for bash 
            // procInfo.Arguments = argss;        // The Script name 
            // procInfo.Verb = verb;                 // ------------ 
            // Process.Start(procInfo);              // Start that process.
            // // Process process = new Process
            // {
            //     StartInfo = new ProcessStartInfo
            //     {
            //         FileName = "bash",
            //         RedirectStandardInput = true,
            //         RedirectStandardOutput = true,
            //         RedirectStandardError = true,
            //         UseShellExecute = false
            //     }
            // };
            // ProcessStartInfo processInfo;
            // Process process;
            //
            // processInfo = new ProcessStartInfo("bin/bash", "-c 'ls -l" + "dotnet run --project ../../../../Dima.Web/Dima.Web.csproj");
            // processInfo.CreateNoWindow = true;
            // processInfo.UseShellExecute = true;
            //
            // process = Process.Start(processInfo);
            // new ProcessStartInfo(, "-c 'ls -l'");
            // process.Start();
            // await process.StandardInput.WriteLineAsync("dotnet run --project ../../../../Dima.Web/Dima.Web.csproj \n");
            // ProcessStartInfo processInfo;
            // Process process;
            //
            // processInfo = new ProcessStartInfo("cmd.exe", "/K " + "dotnet run --project ../../../../Dima.Web/Dima.Web.csproj");
            // processInfo.CreateNoWindow = true;
            // processInfo.UseShellExecute = true;
            //
            // process = Process.Start(processInfo);

            await Task.Delay(10000);

            //using var host = new BlazorApplicationFactory<Program>();

            using IPlaywright playwright = await Playwright.CreateAsync();
            await using IBrowser? browser = await playwright.Chromium.LaunchAsync();
            BrowserNewContextOptions contextOptions = new BrowserNewContextOptions
            {
                // Assigns the base address of the host
                // (cannot be hardcoded due to random chosen port)
                BaseURL = "http://localhost:5028",
                // BAF/WAF uses dotnet dev-cert for HTTPS. If
                // that is not trusted on your CI pipeline, this ensures
                // that tests will continue working.
                IgnoreHTTPSErrors = true,
            };

            IBrowserContext context = await browser.NewContextAsync(contextOptions);
            IPage page = await context.NewPageAsync();


            await page.GotoAsync(
                "/",
            new PageGotoOptions()
            {
                WaitUntil = WaitUntilState.NetworkIdle
            });
            //Assert.Fail();
        }
    }
    
    public static class DllHelper
    {
        [System.Runtime.InteropServices.DllImport("Dima.Web.dll")]
        public static extern int DimaWeb();
        
        [System.Runtime.InteropServices.DllImport("Dima.Web.dll")]
        public static extern int DimaCore();
    }
    

}