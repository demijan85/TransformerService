using Microsoft.AspNetCore.Mvc;
using Transformer.Api.Models;
using Transformer.Api.Services.Interfaces;

namespace Transformer.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransformController : ControllerBase
{
    private readonly ITransformationService _transformationService;

    public TransformController(ITransformationService transformationService)
    {
        _transformationService = transformationService;
    }

    [HttpPost]
    public IActionResult Transform([FromBody] TransformRequest request)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        try
        {
            var response = _transformationService.TransformElements(request);
            return Ok(response);
        }
        catch (InvalidOperationException ex)
        {
            return StatusCode(500, $"An error occurred during transformation: {ex.Message}");
        }
    }
}