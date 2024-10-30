using Transformer.Api.Services.Implementations;

namespace TransformerService.Tests.UnitTests;

public class RegexRemovalTransformerTests
{
    [Fact]
    public void Transform_RemovesMatchingPatterns()
    {
        // Arrange
        var transformer = new RegexRemovalTransformer();
        var input = "Text with numbers 12345";
        var parameters = new Dictionary<string, string>
        {
            { "regex", "\\d+" }
        };

        // Act
        var result = transformer.Transform(input, parameters);

        // Assert
        Assert.Equal("Text with numbers ", result);
    }

    [Fact]
    public void Transform_InvalidRegex_ThrowsArgumentException()
    {
        // Arrange
        var transformer = new RegexRemovalTransformer();
        var input = "Sample text";
        var parameters = new Dictionary<string, string>
        {
            { "regex", "[" } // Invalid regex
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() =>
        {
            transformer.Transform(input, parameters);
        });
    }

    [Fact]
    public void Transform_MissingRegexParameter_ThrowsArgumentException()
    {
        // Arrange
        var transformer = new RegexRemovalTransformer();
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
