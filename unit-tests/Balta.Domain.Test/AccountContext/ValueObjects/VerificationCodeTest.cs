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

    [Fact(DisplayName = "ShouldGenerateVerificationCode")]
    public void GivenValidIDateTimeProvider_ShouldGenerate_ShouldReturnVerificationCode()
    {
        // Arrange
        // Act
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Assert
        verificationCode.Should().NotBeNull();
    }

    [Fact(DisplayName = "ShouldGenerateVerificationCode")]
    public void GivenNoDateTimeProvider_ShouldGenerate_ShouldReturnVerificationCode()
    {
        // Arrange
        // Act
        var verificationCode = VerificationCode.ShouldCreate();

        // Assert
        verificationCode.Should().NotBeNull();
    }

    [Fact(DisplayName = "ShouldGenerateExpiresAtInFuture")]
    public void GivenValidVerificationCode_ExpirationDate_ShouldBeFutureDate()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        var expirationDate = verificationCode.ExpiresAtUtc;

        // Assert
        expirationDate.Should().BeAfter(DateTime.UtcNow);
    }

    [Fact(DisplayName = "ShouldGenerateVerifiedAtAsNull")]
    public void GivenValidVerificationCode_VerifiedAt_ShouldBeNull()
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        // Assert
        verificationCode.VerifiedAtUtc.Should().BeNull();
    }

    [Fact(DisplayName = "ShouldBeInactiveWhenCreated")]
    public void GivenValidVerificationCode_IsActive_ShouldReturnFalse() 
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        // Assert
        verificationCode.IsActive.Should().BeFalse();
    }

    [Fact(DisplayName = "ShouldFailIfExpired ShouldFailIfIsNotActive")]
    public void GivenExpiredVerificationCode_ShouldVerify_ShouldThrowException() 
    {
        // Arrange
        _dateTimeProviderMock.Setup(x=>x.ExpirationDate).Returns(DateTime.UtcNow.AddDays(-1));
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        var verify = () => verificationCode.ShouldVerify(Guid.NewGuid().ToString());

        // Assert
        verify.Should()
            .ThrowExactly<InvalidVerificationCodeException>();
    }

    [Theory(DisplayName = "ShouldFailIfCodeIsInvalid ShouldFailIfCodeIsLessThanSixChars")]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("xpto")]
    public void GivenInvalidCode_ShouldVerify_ShouldThrowException(string invalidCode)
    {
        // Arrange
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);

        // Act
        var verify = () => verificationCode.ShouldVerify(invalidCode);


        // Assert
        verify.Should()
            .ThrowExactly<InvalidVerificationCodeException>();

    }

    [Fact(DisplayName = "ShouldFailIfIsAlreadyVerified")]
    public void GivenAlreadyVerifiedCode_ShouldVerify_ShouldThrowException()
    {
        // Arrange
        var code = "xptoxpto";
        var verificationCode = VerificationCode.ShouldCreate(_dateTimeProviderMock.Object);
        verificationCode.ShouldVerify(code);

        // Act
        var verify = () => verificationCode.ShouldVerify(code);


        // Assert
        verify.Should()
            .ThrowExactly<InvalidVerificationCodeException>();
    }
}