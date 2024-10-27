using Dima.E2ETests.ExecutionOrderer;
using Dima.E2ETests.Infrastructure.Applications;

namespace Dima.E2ETests;

public partial class E2ETest
{
    [Fact(DisplayName = "Register should redirect to login page"), OrderOfExecution(1)]
    public async Task GivenValidEmailAndPassword_Register_ShouldRedirectToLoginPage()
    {
        // Arrange
        await NavigateTo($"{WebAppHandler.Url}/comecar");

        // Act
        await FindAndInsert("email", Email);
        await FindAndInsert("password", Password);
        await Submit();


        // Assert
        Assert.Equal(driver.Url, $"{WebAppHandler.Url}/login");
    }
}
