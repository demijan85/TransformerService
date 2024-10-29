using Transformer.Api.Services.Implementations;

namespace TransformerService.Tests.UnitTests;

public class CyrillicGreekToLatinTransformerTests
{
    [Fact]
    public async Task TransformAsync_ConvertsCyrillicToLatin()
    {
        // Arrange
        var transformer = new CyrillicGreekToLatinTransformer();
        var input = "Пример текста";

        // Act
        var result = await transformer.TransformAsync(input, null);

        // Assert
        Assert.Equal("Primer teksta", result);
    }

    [Fact]
    public async Task TransformAsync_ConvertsGreekToLatin()
    {
        // Arrange
        var transformer = new CyrillicGreekToLatinTransformer();
        var input = "Παρaδειγμα κειμενου";

        // Act
        var result = await transformer.TransformAsync(input, null);

        // Assert
        Assert.Equal("Paradeigma keimenoy", result);
    }

    [Fact]
    public async Task TransformAsync_IgnoresNonCyrillicGreekCharacters()
    {
        // Arrange
        var transformer = new CyrillicGreekToLatinTransformer();
        var input = "Sample text";

        // Act
        var result = await transformer.TransformAsync(input, null);

        // Assert
        Assert.Equal("Sample text", result);
    }
}