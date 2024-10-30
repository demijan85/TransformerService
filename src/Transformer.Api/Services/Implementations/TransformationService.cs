using Transformer.Api.Models;
using Transformer.Api.Services.Interfaces;

namespace Transformer.Api.Services.Implementations;

public class TransformationService : ITransformationService
{
    private readonly ITransformerFactory _transformerFactory;

    public TransformationService(ITransformerFactory transformerFactory)
    {
        _transformerFactory = transformerFactory;
    }

    public TransformResponse TransformElements(TransformRequest request)
    {
        var response = new TransformResponse
        {
            Results = new List<ResponseModel>()
        };

        foreach (var element in request.Elements)
        {
            var originalValue = element.Value;
            var transformedValue = originalValue;

            try
            {
                foreach (var transformerModel in element.Transformers)
                {
                    var transformer = _transformerFactory
                        .GetTransformer(transformerModel.GroupId, transformerModel.TransformerId);

                    transformedValue = transformer.Transform(transformedValue, transformerModel.Parameters);
                }

                response.Results.Add(new ResponseModel
                {
                    OriginalValue = originalValue,
                    TransformedValue = transformedValue
                });
            }
            catch (Exception ex)
            {
                throw new InvalidOperationException(
                    $"An error occurred while processing the element with value '{originalValue}': {ex.Message}", ex);
            }
        }

        return response;
    }
}