using System.Text.RegularExpressions;
using Transformer.Api.Attributes;

namespace Transformer.Api.Services.Implementations;

[Transformer("Group1", "RegexRemoval")]
public class RegexRemovalTransformer : BaseTransformer
{
    protected override string PerformTransformation(string input, Dictionary<string, string> parameters)
    {
        var regexPattern = GetParameter(parameters, "regex");
        var regex = new Regex(regexPattern);
        var result = regex.Replace(input, string.Empty);
        return result;
    }
}