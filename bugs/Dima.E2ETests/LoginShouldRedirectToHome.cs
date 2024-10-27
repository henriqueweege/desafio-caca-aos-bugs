using Dima.E2ETests.ExecutionOrderer;
using Dima.E2ETests.Infrastructure.Applications;

namespace Dima.E2ETests;

public partial class E2ETest
{
    [Fact(DisplayName = "Login should redirect to home page"), OrderOfExecution(2)]
    public async Task GivenValidUser_Login_ShouldRedirectToHome()
    {
        // Arrange
        await NavigateTo($"{WebAppHandler.Url}/login");

        // Act
        await FindAndInsert("email", Email);
        await FindAndInsert("password", Password);
        await Submit();


        // Assert
        Assert.Equal(driver.Url, $"{WebAppHandler.Url}/");
    }
}
