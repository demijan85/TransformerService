namespace Transformer.Api.Services.Interfaces;

public interface ITransformer
{
    string Transform(string input, Dictionary<string, string> parameters);
}