using System.Text.RegularExpressions;
using Transformer.Api.Services.Interfaces;

namespace Transformer.Api.Services.Implementations;

public abstract class BaseTransformer : ITransformer
{
    public string Transform(string input, Dictionary<string, string> parameters)
    {
        if (string.IsNullOrEmpty(input))
        {
            return input;
        }

        try
        {
            return PerformTransformation(input, parameters);
        }
        catch (ArgumentException ex)
        {
            throw new ArgumentException("Invalid regex pattern.", ex);
        }
    }
    
    public abstract string PerformTransformation(string input, Dictionary<string, string> parameters);

    protected string GetParameter(Dictionary<string, string> parameters, string key)
    {
        if (parameters.TryGetValue(key, out var value))
        {
            return value;
        }

        throw new ArgumentException($"Parameter '{key}' is required for this transformer.");
    }
    
}