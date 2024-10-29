using System.Text.RegularExpressions;
using Transformer.Api.Attributes;

namespace Transformer.Api.Services.Implementations;

[Transformer("Group1", "RegexReplacement")]
public class RegexReplacementTransformer : BaseTransformer
{
    public override Task<string> TransformAsync(string input, Dictionary<string, string> parameters)
    {
        var regexPattern = GetParameter(parameters, "regex");
        var replacement = GetParameter(parameters, "replacement");

        try
        {
            var regex = new Regex(regexPattern);
            var result = regex.Replace(input, replacement);
            return Task.FromResult(result);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("Invalid regex pattern.", ex);
        }
    }
}