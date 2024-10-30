using Transformer.Api.Models;

namespace Transformer.Api.Services.Interfaces;

public interface ITransformationService
{
    TransformResponse TransformElements(TransformRequest request);
}