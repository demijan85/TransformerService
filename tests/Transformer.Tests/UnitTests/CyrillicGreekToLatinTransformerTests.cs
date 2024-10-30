using Transformer.Api.Services.Implementations;

namespace TransformerService.Tests.UnitTests;

public class CyrillicGreekToLatinTransformerTests
{
    [Fact]
    public void Transform_ConvertsCyrillicToLatin()
    {
        // Arrange
        var transformer = new CyrillicGreekToLatinTransformer();
        var input = "Пример текста";

        // Act
        var result = transformer.Transform(input, null);

        // Assert
        Assert.Equal("Primer teksta", result);
    }

    [Fact]
    public void Transform_ConvertsGreekToLatin()
    {
        // Arrange
        var transformer = new CyrillicGreekToLatinTransformer();
        var input = "Παρaδειγμα κειμενου";

        // Act
        var result = transformer.Transform(input, null);

        // Assert
        Assert.Equal("Paradeigma keimenoy", result);
    }

    [Fact]
    public void Transform_IgnoresNonCyrillicGreekCharacters()
    {
        // Arrange
        var transformer = new CyrillicGreekToLatinTransformer();
        var input = "Sample text";

        // Act
        var result = transformer.Transform(input, null);

        // Assert
        Assert.Equal("Sample text", result);
    }
}