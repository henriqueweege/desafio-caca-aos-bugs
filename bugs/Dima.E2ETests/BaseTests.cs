using Dima.E2ETests.ExecutionOrderer;
using Dima.E2ETests.Infrastructure;
using Dima.E2ETests.Infrastructure.Applications;
using OpenQA.Selenium;

namespace Dima.E2ETests;

[TestCaseOrderer("Dima.E2ETests.ExecutionOrderer.TestsOrganizer", "Dima.E2ETests")]
public partial class E2ETest
{
    private IWebDriver driver;
    private const string Email = "teste@balta.io";
    private const string Password = "Str0ng!";

    public E2ETest()
    {
        InfrastructureHandler.Setup();
        driver = InfrastructureHandler._driver;
    }

    private async Task NavigateTo(string url)
    {
        driver.Navigate().GoToUrl(url);
        await Wait();
    }

    private static async Task Wait()
    {
        await Task.Delay(5000);
    }

    private async Task FindByIdAndClick(string idOfElement)
    {
        if (ElementExistsById(idOfElement))
        {
            await Wait();
            var toClick = driver.FindElement(By.Id(idOfElement));
            await Wait();
            toClick.Click();
            await Wait();

        }
        else
        {
            Assert.Fail($"Something went wrong clicking in {idOfElement}");

        }
    }

    private async Task FindByClassAndClick(string classOfElement)
    {
        if (ElementExistsByClass(classOfElement))
        {
            await Wait();
            var submit = driver.FindElements(By.ClassName(classOfElement)).First();
            await Wait();
            submit.Click();
            await Wait();

        }
        else
        {
            Assert.Fail($"Something went wrong clicking in {classOfElement}");

        }
    }

    private async Task FindByClassAndClickDisplayed(string classOfElement)
    {
        if (ElementExistsByClass(classOfElement))
        {
            await Wait();
            var submit = driver.FindElements(By.ClassName(classOfElement)).First(x=>x.Displayed);
            await Wait();
            submit.Click();
            await Wait();

        }
        else
        {
            Assert.Fail($"Something went wrong clicking in {classOfElement}");

        }
    }

    private async Task FindAndInsert(string idOfElement, string dataToInsert)
    {
        if (ElementExistsById(idOfElement))
        {
            await Wait();
            var element = driver.FindElement(By.Id(idOfElement));
            element.Clear();
            await Wait();
            element.SendKeys(dataToInsert);
        }
        else
        {
            Assert.Fail($"Something went wrong inserting into {idOfElement}");

        }
    }

    private async Task Submit()
    {
       await FindByIdAndClick("submit");
       await Wait();
    }

    private bool ElementExistsById(string id)
    {
        return driver.FindElements(By.Id(id)).Any();
    }

    private bool ElementExistsByClass(string @class)
    {
        return driver.FindElements(By.ClassName(@class)).Any();
    }
}