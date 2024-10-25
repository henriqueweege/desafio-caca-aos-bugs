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

    [Fact]
    public void GivenEmailWithValidFormatAndUpperCase_ShouldCreate_ShouldLowerCaseEmail() //ShouldLowerCaseEmail
    {
        // Arrange
        var upperCase = _emailForTests.ToUpper();

        // Act
        var email = Email.ShouldCreate(upperCase, _dateTimeProviderMock.Object);

        // Assert
        email.Address.Should().NotBeUpperCased();
    }


    [Fact]
    public void GivenEmailWithValidFormatAndBlankSpaceInTheBeginAndEnd_ShouldCreate_ShouldTrimEmail() //ShouldTrimEmail
    {
        // Arrange
        var emailWithWhiteSpaces = $" {_emailForTests} ";

        // Act
        var email = Email.ShouldCreate(emailWithWhiteSpaces, _dateTimeProviderMock.Object);

        // Assert
        email.Address.First().Should().NotBe(' ');
        email.Address.Last().Should().NotBe(' ');
    }


    [Fact]
    public void GivenNullEmail_ShouldCreate_ShouldThrowNullReferenceException() //ShouldFailIfEmailIsNull
    {
        // Arrange
        // Act
        var shouldCreate = () => Email.ShouldCreate(null, _dateTimeProviderMock.Object);

        // Assert
        shouldCreate.Should().Throw<NullReferenceException>();
    }

    
    [Theory]
    [InlineData("notAValidEmail")]
    [InlineData("")]
    public void GivenInvalidEmail_ShouldCreate_ShouldThrowInvalidEmailException(string testCase) //ShouldFailIfEmailIsInvalid //ShouldFailIfEmailIsEmpty
    {
        // Arrange
        // Act
        var shouldCreate = () => Email.ShouldCreate(testCase, _dateTimeProviderMock.Object);

        // Assert
        shouldCreate.Should().Throw<InvalidEmailException>();
    }


    [Fact]
    public void GivenValidEmail_ShouldCreate_ShouldReturnEmail() //ShouldPassIfEmailIsValid
    {
        // Arrange
        // Act
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);

        // Assert
        email.Should().NotBeNull().And.BeOfType<Email>();
        email.Address.Should().NotBeNullOrEmpty().And.NotBeNullOrWhiteSpace();
    }


    [Fact]
    public void GivenValidEmail_ShouldCreate_ShouldHashEmailAddress()
    {
        // Arrange
        // Act
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);


        // Assert
        email.Hash.Should().NotBeNullOrEmpty().And.NotBeNullOrWhiteSpace(); 
    }
    
    [Fact]
    public void GivenValidEmail_ExplicitConvertFromString_ShouldConvertCorrectly() //ShouldExplicitConvertFromString
    {
        // Arrange
        // Act
        var asEmail = (Email)_emailForTests;

        // Assert
        asEmail.Should().NotBeNull();
        asEmail.Address.Should().Be(_emailForTests);
    }

    [Fact]
    public void GivenValidEmail_ExplicitConvertToString_ShouldConvertCorrectly() //ShouldExplicitConvertToString
    {
        // Arrange
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);


        // Act
        var asString = (string)email;


        // Assert
        asString.Should().Be(email.Address);

    }

    [Fact]
    public void GivenValidEmail_ToString_ShouldConvertCorrectly()//ShouldReturnEmailWhenCallToStringMethod
    {
        // Arrange
        var email = Email.ShouldCreate(_emailForTests, _dateTimeProviderMock.Object);


        // Act
        var asString = email.ToString();


        // Assert
        asString.Should().Be(email.Address);
    }
}