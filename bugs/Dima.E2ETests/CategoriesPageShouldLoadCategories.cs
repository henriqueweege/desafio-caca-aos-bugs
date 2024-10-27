using Dima.E2ETests.ExecutionOrderer;
using Dima.E2ETests.Infrastructure.Applications;
using OpenQA.Selenium;

namespace Dima.E2ETests;

public partial class E2ETest
{
    [Fact(DisplayName = "Categories page should load"), OrderOfExecution(3)]
    public async Task GivenUserWithCategories_CategoriesPage_ShouldLoadCategories()
    {
        // Arrange
        await NavigateTo($"{WebAppHandler.Url}/categorias");

        // Act
        var categories = driver.FindElement(By.ClassName("mud-table-body")).Text;

        // Assert
        Assert.Contains("Beleza", categories);
        Assert.Contains("Alimentação", categories);
        Assert.Contains("Educação", categories);
        Assert.Contains("Fitness", categories);
        Assert.Contains("Impostos", categories);
        Assert.Contains("Investimentos", categories);
        Assert.Contains("Lazer", categories);
        Assert.Contains("Moradia", categories);
        Assert.Contains("Pets", categories);
        Assert.Contains("Presentes", categories);
        Assert.Contains("Roupas", categories);
        Assert.Contains("Saúde", categories);
        Assert.Contains("Saúde Mental", categories);
    }
}
