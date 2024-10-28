using Dima.Core;
using Dima.E2ETests.Infrastructure.Applications;
using Dima.E2ETests.Infrastructure.Database;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Diagnostics;

namespace Dima.E2ETests.Infrastructure
{
    internal static class InfrastructureHandler 
    {
        public static IWebDriver _driver;

        private static bool NeedsSetup = true;
        public static void Setup()
        {
            if(NeedsSetup)
            {
                Environment.SetEnvironmentVariable(Configuration.E2ETestEnv, "true");

                //DatabaseHandler.StartAsync().GetAwaiter().GetResult();

                ProcessStartInfo processInfo;

                processInfo = new ProcessStartInfo("docker build -t dockerfile . docker run -d -p 37000:1433 --name my-mssql-container dockerfile");
                processInfo.UseShellExecute = true;

                Process.Start(processInfo);


                WebAppHandler.RunWebApp();
                ApiHandler.RunApi();

                _driver = new ChromeDriver();

                Thread.Sleep(20000);
                NeedsSetup = false;
            } 
        }

        public static void DisposeInfrastructure()
        {
            Environment.SetEnvironmentVariable(Configuration.E2ETestEnv, null);
            WebAppHandler.DisposeApp();
            ApiHandler.DisposeApp();
        }
    }
}
