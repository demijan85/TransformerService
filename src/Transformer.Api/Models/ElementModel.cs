using System.ComponentModel.DataAnnotations;

namespace Transformer.Api.Models;

public class ElementModel
{
    [Required]
    public string Value { get; set; }

    [Required]
    public List<TransformerModel> Transformers { get; set; }
}