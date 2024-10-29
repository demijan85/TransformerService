using Microsoft.AspNetCore.Mvc;
using Transformer.Api.Models;
using Transformer.Api.Services.Interfaces;

namespace Transformer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransformController : ControllerBase
{
    private readonly ITransformerFactory _transformerFactory;

    public TransformController(ITransformerFactory transformerFactory)
    {
        _transformerFactory = transformerFactory;
    }

    [HttpPost]
    public async Task<IActionResult> Transform([FromBody] TransformRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

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
                    var transformer = await _transformerFactory
                        .GetTransformerAsync(transformerModel.GroupId, transformerModel.TransformerId);

                    transformedValue = await transformer.TransformAsync(transformedValue, transformerModel.Parameters);
                }

                response.Results.Add(new ResponseModel
                {
                    OriginalValue = originalValue,
                    TransformedValue = transformedValue
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing the element: {ex.Message}");
            }
        }

        return Ok(response);
    }
}