using Dima.Core;
using Dima.E2ETests.Infrastructure.Applications;
using Dima.E2ETests.Infrastructure.Database;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

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
                DatabaseHandler.StartAsync().GetAwaiter().GetResult();
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
