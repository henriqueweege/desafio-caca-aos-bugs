using Dima.Core;
using Dima.E2ETests.Infrastructure.Applications;
using Dima.E2ETests.Infrastructure.Database;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;

namespace Dima.E2ETests.Infrastructure;

internal static class InfrastructureHandler 
{
    public static IWebDriver _driver;

    private static bool NeedsSetup = true;
    public static void Setup()
    {
        if(NeedsSetup)
        {
            DatabaseHandler.StartAsync().GetAwaiter().GetResult();

            WebAppHandler.RunWebApp();
            ApiHandler.RunApi();

            var chromeOpt = new ChromeOptions();
            chromeOpt.AddArgument("-headless");
            _driver = new ChromeDriver(chromeOpt);
            
            Thread.Sleep(20000);
            NeedsSetup = false;
        } 
    }
}
