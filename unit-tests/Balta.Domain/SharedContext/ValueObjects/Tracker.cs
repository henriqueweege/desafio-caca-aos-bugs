using Balta.Domain.SharedContext.Abstractions;

namespace Balta.Domain.SharedContext.ValueObjects;

public sealed record Tracker : ValueObject
{
    #region Constructors

    private Tracker(DateTime createdAtUtc, DateTime updatedAtUtc)
    {
        CreatedAtUtc = createdAtUtc;
        UpdatedAtUtc = updatedAtUtc;
    }

    #endregion

    #region Factories

    public static Tracker ShouldCreate(IDateTimeProvider dateTimeProvider)
        => new(dateTimeProvider.ExpirationDate, dateTimeProvider.ExpirationDate);

    #endregion

    #region Properties

    public DateTime CreatedAtUtc { get; } // = dateTimeProvider.UtcNow;

    public DateTime UpdatedAtUtc { get; }

    #endregion
}