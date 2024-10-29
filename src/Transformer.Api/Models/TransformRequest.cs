using System.ComponentModel.DataAnnotations;

namespace Transformer.Api.Models;

public class TransformRequest
{
    [Required]
    public List<ElementModel> Elements { get; set; }
}