namespace Balta.Domain.SharedContext.Abstractions;

public interface IDateTimeProvider
{
    #region Properties

    DateTime ExpirationDate { get; }

    #endregion
}