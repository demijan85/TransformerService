using System.Text.RegularExpressions;
using Transformer.Api.Attributes;

namespace Transformer.Api.Services.Implementations;

[Transformer("Group1", "RegexRemoval")]
public class RegexRemovalTransformer : BaseTransformer
{
    public override Task<string> TransformAsync(string input, Dictionary<string, string> parameters)
    {
        if (string.IsNullOrEmpty(input))
        {
            return Task.FromResult(input);
        }
        
        var regexPattern = GetParameter(parameters, "regex");

        try
        {
            var regex = new Regex(regexPattern);
            var result = regex.Replace(input, string.Empty);
            return Task.FromResult(result);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("Invalid regex pattern.", ex);
        }
    }
}