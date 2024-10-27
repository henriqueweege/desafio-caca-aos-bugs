using Dima.E2ETests.ExecutionOrderer;
using Dima.E2ETests.Infrastructure.Applications;
using OpenQA.Selenium;

namespace Dima.E2ETests;

public partial class E2ETest
{


    [Fact(DisplayName = "Transaction updated should appear in transactions page"), OrderOfExecution(6)]
    public async Task GivenUpdatedTransactionCreated_TransactionsPage_ShouldContainUpdatedTransaction()
    {
        // Arrange
        var genericString = "xpto2";
        await NavigateTo($"{WebAppHandler.Url}/lancamentos/historico");
        await FindByClassAndClickDisplayed("edit");

        // Act
        await FindAndInsert("title", genericString);
        await Submit();

        // Assert
        await NavigateTo($"{WebAppHandler.Url}/lancamentos/historico");
        var transactions = driver.FindElement(By.ClassName("mud-table-body")).Text;
        Assert.Contains(genericString, transactions);
    }
}
