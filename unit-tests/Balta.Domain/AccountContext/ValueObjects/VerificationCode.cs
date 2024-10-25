using Balta.Domain.AccountContext.ValueObjects.Exceptions;
using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.AccountContext.ValueObjects;

public class VerificationCode
{
    #region Constants

    private const int MinLength = 6;

    #endregion
    
    #region Constructors

    private VerificationCode(string code, DateTime expiresAtUtc)
    {
        Code = code;
        ExpiresAtUtc = expiresAtUtc;
    }
    
    #endregion

    #region Factories

    public static VerificationCode ShouldCreate(IDateTimeProvider? dateTimeProvider = null)
        => dateTimeProvider is null? ShouldCreateFromUtcNow() : new (Guid.NewGuid().ToString("N")[..MinLength].ToUpper(),
            dateTimeProvider.ExpirationDate.AddMinutes(5));

    private static VerificationCode ShouldCreateFromUtcNow()
        => new(Guid.NewGuid().ToString("N")[..MinLength].ToUpper(),
            DateTime.UtcNow.AddMinutes(5));

    #endregion

    #region Properties

    public string Code { get; }
    public DateTime? ExpiresAtUtc { get; private set; }
    public DateTime? VerifiedAtUtc { get; private set; }
    public bool IsActive { get; private set; } = false; 

    #endregion

    #region Public Methods

    public void Expired() => ExpiresAtUtc = DateTime.UtcNow; 

    public void ShouldVerify(string code)
    {

        if (InvalidCode(code) || IsExpired() || AlreadyVerified())
            throw new InvalidVerificationCodeException();
        
        VerifiedAtUtc = DateTime.UtcNow;
        IsActive = true;
    }

    #endregion

    #region Private Methods

    private static bool InvalidCode(string code)
    {
        return string.IsNullOrEmpty(code) || string.IsNullOrWhiteSpace(code) || code.Length < MinLength;
    }

    private bool AlreadyVerified()
    {
        return VerifiedAtUtc != null;
    }

    private bool IsExpired()
    {
        return ExpiresAtUtc <= DateTime.UtcNow;
    }

    #endregion

    #region Operators

    public static implicit operator string(VerificationCode verificationCode) => verificationCode.ToString();
    
    #endregion

    #region Others

    public override string ToString() => Code;

    #endregion
}