using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System.Diagnostics;

namespace Dima.E2ETests
{
    public class UnitTest1
    {
        private IWebDriver driver;
        public UnitTest1()
        {
            RunWebProject().Wait();

            driver = (IWebDriver)new ChromeDriver();
        }
        private static async Task RunWebProject()
        {
            ProcessStartInfo processInfo;
            Process process;

            processInfo = new ProcessStartInfo("cmd.exe", "/K " + "dotnet run --project ../../../../Dima.Web/Dima.Web.csproj");
            processInfo.CreateNoWindow = true;
            processInfo.UseShellExecute = true;

            process = Process.Start(processInfo);

            await Task.Delay(20000);
        }

        [Fact]
        public async Task ValidateTheMessageIsDisplayed()
        {
            driver.Navigate().GoToUrl("http://localhost:5028/comecar");
            await Task.Delay(10000);
            driver.FindElement(By.Id("email")).SendKeys("teste@mail.com");
            driver.FindElement(By.Id("password")).SendKeys("xpto");
            driver.FindElement(By.Id("submit")).Click();
            await Task.Delay(2000);
            Assert.Equal(driver.Url, "http://localhost:5028/comecar");
        }

    }
}