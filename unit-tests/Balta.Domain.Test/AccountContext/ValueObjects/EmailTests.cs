using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using FluentAssertions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class EmailTests
{

    private const string  _emailForTests = "mail@tests.com";
    private readonly Mock<IDateTimeProvider> _dateTimeProviderMock;

    public EmailTests()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _dateTimeProviderMock.Setup(x=>x.ExpirationDate).Returns(DateTime.UtcNow);
    }

    [Fact(DisplayName = "ShouldLowerCaseEmail")]
    public void GivenEmailWithValidFormatAndUpperCase_ShouldCreate_ShouldLowerCaseEmail()
    {
        // Arrange
        var upperCase = _emailForTests.ToUpper();

        // Act
        var email = Email.ShouldCreate(upperCase, _dateTimeProviderMock.Object);

        // Assert
        email.Address
            .Should()
            .NotBeUpperCased();
    }


    [Fact(DisplayName = "ShouldTrimEmail")]
    public void GivenEmailWithValidFormatAndBlankSpaceInTheBeginAndEnd_ShouldCreate_ShouldTrimEmail()
    {
        // Arrange
        var emailWithWhiteSpaces = $" {_emailForTests} ";

        // Act
        var email = Email.ShouldCreate(emailWithWhiteSpaces, _dateTimeProviderMock.Object);

        // Assert
        email.Address
            .First()
            .Should()
            .NotBe(' ');

        email.Address
            .Last()
            .Should()
            .NotBe(' ');
    }


    [Fact(DisplayName = "ShouldFailIfEmailIsNull")]
    public void GivenNullEmail_ShouldCreate_ShouldThrowNullReferenceException() 
    {
        // Arrange
        // Act
        var shouldCreate = () => Email.ShouldCreate(null, _dateTimeProviderMock.Object);

        // Assert
        shouldCreate
            .Should()
            .Throw<NullReferenceException>();
    }

    
    [Theory(DisplayName = "ShouldFailIfEmailIsInvalid ShouldFailIfEmailIsEmpty")]
    [InlineData("notAValidEmail")]
    [InlineData("")]
    public void GivenInvalidEmail_ShouldCreate_ShouldThrowInvalidEmailException(string testCase)
    {
        // Arrange
        // Act
        var shouldCreate = () => Email.ShouldCreate(testCase, _dateTimeProviderMock.Object);

        // Assert
        shouldCreate
            .Should()
            .Throw<InvalidEmailException>();
    }


    [Fact(DisplayName = "ShouldPassIfEmailIsValid")]
    public void GivenValidEmail_ShouldCreate_ShouldReturnEmail()
    {
        // Arrange
        // Act
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);

        // Assert
        email.Should()
            .NotBeNull()
            .And
            .BeOfType<Email>();
        
        
        email.Address
            .Should()
            .NotBeNullOrEmpty()
            .And
            .NotBeNullOrWhiteSpace();
    }


    [Fact(DisplayName = "ShouldHashEmailAddress")]
    public void GivenValidEmail_ShouldCreate_ShouldHashEmailAddress() 
    {
        // Arrange
        // Act
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);


        // Assert
        email.Hash
            .Should()
            .NotBeNullOrEmpty()
            .And
            .NotBeNullOrWhiteSpace(); 
    }
    
    [Fact(DisplayName = "ShouldExplicitConvertFromString")]
    public void GivenValidEmail_ExplicitConvertFromString_ShouldConvertCorrectly()
    {
        // Arrange
        // Act
        var asEmail = (Email)_emailForTests;

        // Assert
        asEmail.Should().NotBeNull();

        asEmail.Address.Should().Be(_emailForTests);
    }

    [Fact(DisplayName = "ShouldExplicitConvertToString")]
    public void GivenValidEmail_ExplicitConvertToString_ShouldConvertCorrectly()
    {
        // Arrange
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);


        // Act
        var asString = (string)email;


        // Assert
        asString.Should().Be(email.Address);

    }

    [Fact(DisplayName = "ShouldReturnEmailWhenCallToStringMethod")]
    public void GivenValidEmail_ToString_ShouldConvertCorrectly()
    {
        // Arrange
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);


        // Act
        var asString = email.ToString();


        // Assert
        asString.Should().Be(email.Address);
    }
}