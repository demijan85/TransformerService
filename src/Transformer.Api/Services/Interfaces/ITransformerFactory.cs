namespace Transformer.Api.Services.Interfaces;

public interface ITransformerFactory
{
    ITransformer GetTransformer(string groupId, string transformerId);
}