using Balta.Domain.AccountContext.ValueObjects;
using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;
using FluentAssertions;
using Moq;

namespace Balta.Domain.Test.AccountContext.ValueObjects;

public class VerificationCodeTest
{
    private Mock<IDateTimeProvider> _dateTimeProviderMock;

    public VerificationCodeTest()
    {
        _dateTimeProviderMock = new Mock<IDateTimeProvider>();
        _dateTimeProviderMock.Setup(x => x.ExpirationDate).Returns(DateTime.UtcNow);
    }

    [Fact]
    public void GivenValidIDateTimeProvider_ShouldGenerate_ShouldReturnVerificationCode()//ShouldGenerateVerificationCode
    {
        // Arrange
        // Act
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Assert
        verificationCode.Should().NotBeNull();
    }

    [Fact]
    public void GivenNoDateTimeProvider_ShouldGenerate_ShouldReturnVerificationCode()//ShouldGenerateVerificationCode
    {
        // Arrange
        // Act
        var verificationCode = VerificationCode.ShouldCreate();

        // Assert
        verificationCode.Should().NotBeNull();
    }

    [Fact]
    public void GivenValidVerificationCode_ExpirationDate_ShouldBeFutureDate()//ShouldGenerateExpiresAtInFuture
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        var expirationDate = verificationCode.ExpiresAtUtc;

        // Assert
        expirationDate.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact]
    public void GivenValidVerificationCode_VerifiedAt_ShouldBeNull()//ShouldGenerateVerifiedAtAsNull
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        // Assert
        verificationCode.VerifiedAtUtc.Should().BeNull();
    }

    [Fact]
    public void GivenValidVerificationCode_IsActive_ShouldReturnFalse() //ShouldBeInactiveWhenCreated
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        // Assert
        verificationCode.IsActive.Should().BeFalse();
    }

    [Fact]
    public void GivenExpiredVerificationCode_ShouldVerify_ShouldThrowException() //ShouldFailIfExpired ShouldFailIfIsNotActive
    {
        // Arrange
        _dateTimeProviderMock.Setup(x=>x.ExpirationDate).Returns(DateTime.UtcNow.AddDays(-1));
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        var verify = () => verificationCode.ShouldVerify(Guid.NewGuid().ToString());

        // Assert
        verify.Should().ThrowExactly<InvalidVerificationCodeException>();
    }

    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("xpto")]
    public void GivenInvalidCode_ShouldVerify_ShouldThrowException(string invalidCode)//ShouldFailIfCodeIsInvalid ShouldFailIfCodeIsLessThanSixChars
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        var verify = () => verificationCode.ShouldVerify(invalidCode);


        // Assert
        verify.Should().ThrowExactly<InvalidVerificationCodeException>();

    }

    [Fact]
    public void GivenAlreadyVerifiedCode_ShouldVerify_ShouldThrowException()//ShouldFailIfIsAlreadyVerified
    {
        // Arrange
        var code = "xptoxpto";
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);
        verificationCode.ShouldVerify(code);

        // Act
        var verify = () => verificationCode.ShouldVerify(code);


        // Assert
        verify.Should().ThrowExactly<InvalidVerificationCodeException>();
    }
}