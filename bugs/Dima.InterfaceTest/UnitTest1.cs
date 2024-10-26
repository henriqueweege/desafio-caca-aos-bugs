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

            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/K " + "dotnet run --project ../../../../Dima.Web/Dima.Web.csproj");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = true;

            process = Process.Start(processInfo);

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
}