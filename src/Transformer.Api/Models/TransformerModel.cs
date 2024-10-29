using System.ComponentModel.DataAnnotations;

namespace Transformer.Api.Models;

public class TransformerModel
{
    [Required]
    public string GroupId { get; set; }

    [Required]
    public string TransformerId { get; set; }

    public Dictionary<string, string> Parameters { get; set; }
}