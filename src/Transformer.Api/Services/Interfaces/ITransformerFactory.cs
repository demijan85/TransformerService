namespace Transformer.Api.Services.Interfaces;

public interface ITransformerFactory
{
    Task<ITransformer> GetTransformerAsync(string groupId, string transformerId);
}