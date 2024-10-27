using Dima.E2ETests.ExecutionOrderer;
using Dima.E2ETests.Infrastructure.Applications;
using OpenQA.Selenium;

namespace Dima.E2ETests;

public partial class E2ETest
{

    [Fact(DisplayName = "New transaction should appear in transactions page"), OrderOfExecution(5)]
    public async Task GivenNewTransactionCreated_TransactionsPage_ShouldContainNewTransaction()
    {
        // Arrange
        var genericString = "xpto";
        await NavigateTo($"{WebAppHandler.Url}/lancamentos/novo");

        // Act
        await FindAndInsert("title", genericString);
        await FindAndInsert("amount", "10,00");
        await FindByIdAndClick("paidOrReceivedAt");
        await FindByClassAndClickDisplayed("mud-picker-calendar-day");
        await Submit();

        // Assert
        await NavigateTo($"{WebAppHandler.Url}/lancamentos/historico");
        var transactions = driver.FindElement(By.ClassName("mud-table-body")).Text;
        Assert.Contains(genericString, transactions);
    }
}
