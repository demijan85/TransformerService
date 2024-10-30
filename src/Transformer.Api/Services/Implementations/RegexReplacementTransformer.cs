using System.Text.RegularExpressions;
using Transformer.Api.Attributes;

namespace Transformer.Api.Services.Implementations;

[Transformer("Group1", "RegexReplacement")]
public class RegexReplacementTransformer : BaseTransformer
{
    protected override string PerformTransformation(string input, Dictionary<string, string> parameters)
    {
        var regexPattern = GetParameter(parameters, "regex");
        var replacement = GetParameter(parameters, "replacement");

        var regex = new Regex(regexPattern);
        var result = regex.Replace(input, replacement);
        return result;
    }
}