using Transformer.Api.Services.Implementations;

namespace TransformerService.Tests.UnitTests;

public class RegexRemovalTransformerTests
{
    [Fact]
    public async Task TransformAsync_RemovesMatchingPatterns()
    {
        // Arrange
        var transformer = new RegexRemovalTransformer();
        var input = "Text with numbers 12345";
        var parameters = new Dictionary<string, string>
        {
            { "regex", "\\d+" }
        };

        // Act
        var result = await transformer.TransformAsync(input, parameters);

        // Assert
        Assert.Equal("Text with numbers ", result);
    }

    [Fact]
    public async Task TransformAsync_InvalidRegex_ThrowsArgumentException()
    {
        // Arrange
        var transformer = new RegexRemovalTransformer();
        var input = "Sample text";
        var parameters = new Dictionary<string, string>
        {
            { "regex", "[" } // Invalid regex
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await transformer.TransformAsync(input, parameters);
        });
    }

    [Fact]
    public async Task TransformAsync_MissingRegexParameter_ThrowsArgumentException()
    {
        // Arrange
        var transformer = new RegexRemovalTransformer();
        var input = "Sample text";
        var parameters = new Dictionary<string, string>(); // Empty parameters

        // Act & Assert
        var ex = await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await transformer.TransformAsync(input, parameters);
        });

        Assert.Contains("Parameter 'regex' is required", ex.Message);
    }
}
