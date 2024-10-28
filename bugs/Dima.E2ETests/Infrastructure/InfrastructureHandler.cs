using Dima.Core;
using Dima.E2ETests.Infrastructure.Applications;
using Dima.E2ETests.Infrastructure.Database;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Diagnostics;
using CreditAccountingService.FunctionalTests;

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
                ContainerKiller.KillUpContainers();
                Environment.SetEnvironmentVariable(Configuration.E2ETestEnv, "true");

                DatabaseHandler.StartAsync().GetAwaiter().GetResult();

                WebAppHandler.RunWebApp();
                ApiHandler.RunApi();

                _driver = new ChromeDriver();

                Thread.Sleep(20000);
                NeedsSetup = false;
            } 
        }
    }
}
