using Balta.Domain.SharedContext.Extensions;
using FluentAssertions;
using System.Text;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Theory(DisplayName = "ShouldGenerateBase64FromString"), MemberData(nameof(GetStringToConvert))]
    public void GivenString_ToBase64_ShouldConvertCorrectly(string toConvert)
    {
        // Arrange
        var expected = Convert.ToBase64String(Encoding.ASCII.GetBytes(toConvert));

        // Act
        var converted = toConvert.ToBase64();

        // Assert
        converted.Should().Be(expected);

    }

    public static IEnumerable<object[]?> GetStringToConvert()
    {
        yield return new object[] { "xpto" };
        yield return new object[] { "XPTO" };
        yield return new object[] { "e6697c6d-fe4b-45dc-a0ad-5e3db3c76bd3" };
        yield return new object[] { "e6697?!@-fe4b-4?!@c-?!@ad-5e3db3c?!@d3" };
        yield return new object[] { "E6697C6D-FE4B-45DC-A0AD-5E3DB3C76BD3" };
        yield return new object[] { "E?!@97C6D-F?!@B-45?!@-A?!@D-5?!@DB?!@76BD3" };
    }
}