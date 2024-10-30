using Transformer.Api.Services.Implementations;

namespace TransformerService.Tests.UnitTests;

public class RegexReplacementTransformerTests
{
    [Fact]
    public void Transform_ReplacesMatchingPatterns()
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
        var result = transformer.Transform(input, parameters);

        // Assert
        Assert.Equal("Replace numbers digits with text", result);
    }

    [Fact]
    public void Transform_InvalidRegex_ThrowsArgumentException()
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
        Assert.Throws<ArgumentException>(() =>
        {
            transformer.Transform(input, parameters);
        });
    }

    [Fact]
    public void Transform_MissingParameters_ThrowsArgumentException()
    {
        // Arrange
        var transformer = new RegexReplacementTransformer();
        var input = "Sample text";
        var parameters = new Dictionary<string, string>(); // Empty parameters

        // Act & Assert
        var ex = Assert.Throws<ArgumentException>(() =>
        {
            transformer.Transform(input, parameters);
        });

        Assert.Contains("Parameter 'regex' is required", ex.Message);
    }
}