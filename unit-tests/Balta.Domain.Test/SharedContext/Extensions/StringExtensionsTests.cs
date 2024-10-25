using Balta.Domain.SharedContext.Extensions;
using FluentAssertions;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Balta.Domain.Test.SharedContext.Extensions;

public class StringExtensionsTests
{
    [Theory]
    [InlineData("xpto")]
    [InlineData("XPTO")]
    [InlineData("e6697c6d-fe4b-45dc-a0ad-5e3db3c76bd3")]
    [InlineData("e6697?!@-fe4b-4?!@c-?!@ad-5e3db3c?!@d3")]
    [InlineData("E6697C6D-FE4B-45DC-A0AD-5E3DB3C76BD3")]
    [InlineData("E?!@97C6D-F?!@B-45?!@-A?!@D-5?!@DB?!@76BD3")]
    public void GivenString_ToBase64_ShouldConvertCorrectly(string toConvert)//ShouldGenerateBase64FromString
    {
        // Arrange
        var expected = Convert.ToBase64String(Encoding.ASCII.GetBytes(toConvert));

        // Act
        var converted = toConvert.ToBase64();

        // Assert
        converted.Should().Be(expected);

    }
}