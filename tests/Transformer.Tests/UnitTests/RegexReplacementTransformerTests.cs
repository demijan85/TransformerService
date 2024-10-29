using Transformer.Api.Services.Implementations;

namespace TransformerService.Tests.UnitTests;

public class RegexReplacementTransformerTests
{
    [Fact]
    public async Task TransformAsync_ReplacesMatchingPatterns()
    {
        // Arrange
        var transformer = new RegexReplacementTransformer();
        var input = "Replace numbers 123 with text";
        var parameters = new Dictionary<string, string>
        {
            { "regex", "\\d+" },
            { "replacement", "digits" }
        };

        // Act
        var result = await transformer.TransformAsync(input, parameters);

        // Assert
        Assert.Equal("Replace numbers digits with text", result);
    }

    [Fact]
    public async Task TransformAsync_InvalidRegex_ThrowsArgumentException()
    {
        // Arrange
        var transformer = new RegexReplacementTransformer();
        var input = "Sample text";
        var parameters = new Dictionary<string, string>
        {
            { "regex", "[" }, // Invalid regex
            { "replacement", "text" }
        };

        // Act & Assert
        await Assert.ThrowsAsync<ArgumentException>(async () =>
        {
            await transformer.TransformAsync(input, parameters);
        });
    }

    [Fact]
    public async Task TransformAsync_MissingParameters_ThrowsArgumentException()
    {
        // Arrange
        var transformer = new RegexReplacementTransformer();
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