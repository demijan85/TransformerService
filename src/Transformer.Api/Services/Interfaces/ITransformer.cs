namespace Transformer.Api.Services.Interfaces;

public interface ITransformer
{
    Task<string> TransformAsync(string input, Dictionary<string, string> parameters);
}