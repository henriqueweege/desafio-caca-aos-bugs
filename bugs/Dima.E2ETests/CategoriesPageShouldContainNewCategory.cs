using Dima.E2ETests.ExecutionOrderer;
using Dima.E2ETests.Infrastructure.Applications;
using OpenQA.Selenium;

namespace Dima.E2ETests;

public partial class E2ETest
{

    [Fact(DisplayName = "New category should appear in categories page"), OrderOfExecution(4)]
    public async Task GivenNewCategoryCreated_CategoriesPage_ShouldContainNewCategory()
    {
        // Arrange

        var genericString = "xpto";
        await NavigateTo($"{WebAppHandler.Url}/categorias/adicionar");

        // Act
        await FindAndInsert("title", genericString);
        await FindAndInsert("description", genericString);
        await Submit();

        // Assert
        await NavigateTo($"{WebAppHandler.Url}/categorias");
        var categories = driver.FindElement(By.ClassName("mud-table-body")).Text;
        Assert.Contains(genericString, categories);
    }
}
