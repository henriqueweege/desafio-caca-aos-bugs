using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using FluentAssertions;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class PasswordTests
{
    private static string ValidPassword = "df6d8caf15fa";
    private const string Special = "!@#$%ˆ&*(){}[];";

    [Theory(DisplayName = "ShouldFailIfPasswordIsNull ShouldFailIfPasswordIsEmpty ShouldFailIfPasswordIsWhiteSpace")]
    [InlineData(null)]
    [InlineData("")]
    [InlineData(" ")]
    public void GivenInvalidPassword_ShouldCreate_ShouldThrowInvalidPasswordExceptionWithMessage(string password)
    {
        // Arrange
        // Act
        var create = () => Password.ShouldCreate(password);

        // Assert
        create.Should()
            .ThrowExactly<InvalidPasswordException>()
            .WithMessage("Password cannot be null or empty");
    }

    [Fact(DisplayName = "ShouldFailIfPasswordLenIsLessThanMinimumChars")]
    public void GivenPasswordShorterThanAllowed_ShouldCreate_ShouldThrowInvalidPasswordExceptionWithMessage()
    {
        // Arrange
        var password = "xpto";

        // Act
        var create = () => Password.ShouldCreate(password);

        // Assert
        create.Should()
            .ThrowExactly<InvalidPasswordException>()
            .WithMessage("Password should have at least 8 characters");
    }


    [Fact(DisplayName = "ShouldFailIfPasswordLenIsGreaterThanMaxChars")]
    public void GivenPasswordLargerThanAllowed_ShouldCreate_ShouldThrowInvalidPasswordExceptionWithMessage() 
    {
        // Arrange
        var password = new string('a', 49);

        // Act
        var create = () => Password.ShouldCreate(password);

        // Assert
        create.Should()
            .ThrowExactly<InvalidPasswordException>()
            .WithMessage("Password should have less than 48 characters");

    }

    [Fact(DisplayName = "ShouldHashPassword")]
    public void GivenValidPassword_ShouldCreate_ShouldHashPassword()
    {
        // Arrange
        // Act
        var hash = Password.ShouldCreate(ValidPassword);

        // Assert
        hash.Should().NotBeNull();

        hash.Hash
            .Should()
            .NotBeNullOrEmpty()
            .And
            .NotBeNullOrWhiteSpace();
    }

    [Fact(DisplayName = "ShouldVerifyPasswordHash")]
    public void GivenValidPassword_ShouldVerify_ShouldNotThrowException()
     {
        // Arrange
        var password = Password.ShouldCreate(ValidPassword);

        // Act
        var res = () => password.ShouldVerify();

        // Assert
        res.Should().NotThrow();
    }

    [Fact(DisplayName = "ShouldVerifyPasswordHash")]
    public void GivenHashAndCorrectPassword_ShouldMatch_ShouldReturnTrue()//
    {
        // Arrange
        var hash = Password.ShouldCreate(ValidPassword).Hash;

        // Act
        var match = Password.ShouldMatch(hash, ValidPassword);

        // Assert
        match.Should().BeTrue();
    }

    [Theory(DisplayName = "ShouldVerifyPasswordHash"), MemberData(nameof(GetMismatchScenarios))]
    public void GivenMismatchBetweenHashAndPassword_ShouldMatch_ShouldReturnFalse(string hash, string password)
    {
        // Arrange
        // Act
        var match = Password.ShouldMatch(hash, password);

        // Assert
        match.Should().BeFalse();
    }

    [Theory(DisplayName = "ShouldGenerateStrongPassword"), MemberData(nameof(GetShouldGenerateScenarios))]    
    public void GivenValidParameters_ShouldGenerate_ShouldGenerateStrongPassword(short length, bool includeSpecialChars, bool upperCase)
    {
        // Arrange
        // Act
        var password = Password.ShouldGenerate(length, includeSpecialChars, upperCase);

        // Assert
        password.Should().HaveLength(length);

        password
            .Any(x => Special.Contains(x))
            .Should()
            .Be(includeSpecialChars);

        password
            .Any(x => char.IsAsciiLetterUpper(x))
            .Should().Be(upperCase);
    }

    [Fact(DisplayName = "ShouldImplicitConvertToString")]
    public void GivenPassword_ImplicitConvertToString_ShouldConvert()
    {
        // Arrange
        var password = Password.ShouldCreate(ValidPassword);

        // Act
        string asString = password;

        // Assert
        asString.Should()
            .NotBeNullOrWhiteSpace()
            .And
            .NotBeNullOrEmpty()
            .And
            .Be(password.Hash);
    }

    [Fact(DisplayName = "ShouldReturnHashAsStringWhenCallToStringMethod")]
    public void GivenPassword_ToString_ShouldReturnHashAsString()
    {
        // Arrange
        var password = Password.ShouldCreate(ValidPassword);

        // Act
        string hash = password.ToString();

        // Assert
        hash.Should()
            .NotBeNullOrWhiteSpace()
            .And
            .NotBeNullOrEmpty()
            .And
            .Be(password.Hash);
    }

    [Fact(DisplayName = "ShouldMarkPasswordAsExpired")]
    public void GivenPassword_MarkAsExpired_ShouldMakeIsActiveReturnFalse()
    {
        // Arrange
        var password = Password.ShouldCreate(ValidPassword);

        // Act
        password.VerificationCode.Expired();

        // Assert
        password.VerificationCode.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = "ShouldFailIfPasswordIsExpired")]
    public void GivenExpiredPassword_ShouldVerify_ShouldThrowException()
    {
        // Arrange
        var password = Password.ShouldCreate(ValidPassword);
        password.VerificationCode.Expired();

        // Act
        var shouldVerify = () => password.ShouldVerify();

        // Assert
        shouldVerify.Should()
            .Throw<InvalidVerificationCodeException>();
    }


    [Fact(DisplayName = "ShouldMarkPasswordAsMustChange")]
    public void GivenPassword_PasswordMustChange_ShouldMarkPasswordAsMustChange()
    {
        // Arrange
        var password = Password.ShouldCreate(ValidPassword);

        // Act
        password.PasswordMustChange();

        // Assert
        password.MustChange.Should().BeTrue();
    }


    [Fact(DisplayName = "ShouldFailIfPasswordIsMarkedAsMustChange")]
    public void GivenPasswordThatMustChange_ShouldVerify_ShouldThrowException()
    {
        // Arrange
        var password = Password.ShouldCreate(ValidPassword);
        password.PasswordMustChange();

        // Act
        var shouldVerify = () => password.ShouldVerify();

        // Assert
        shouldVerify.Should()
            .Throw<InvalidVerificationCodeException>();
    }

    public static IEnumerable<object[]> GetMismatchScenarios()
    {
        yield return new object[] { "xpto", ValidPassword };
        yield return new object[] { Password.ShouldCreate(ValidPassword), "xpto"};
    }

    public static IEnumerable<object[]> GetShouldGenerateScenarios()
    {
        yield return new object[] { 43, false, false };
        yield return new object[] { 100, false, true };
        yield return new object[] { 43, true, false };
        yield return new object[] { 10, true, true };
    }
}