using Transformer.Api.Services.Interfaces;

namespace Transformer.Api.Services.Implementations;

public abstract class BaseTransformer : ITransformer
{
    public abstract Task<string> TransformAsync(string input, Dictionary<string, string> parameters);

    protected string GetParameter(Dictionary<string, string> parameters, string key)
    {
        if (parameters.TryGetValue(key, out var value))
        {
            return value;
        }

        throw new ArgumentException($"Parameter '{key}' is required for this transformer.");
    }
    
}