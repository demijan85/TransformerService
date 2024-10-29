namespace Transformer.Api.Attributes;

[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class TransformerAttribute : Attribute
{
    public string GroupId { get; }
    public string TransformerId { get; }

    public TransformerAttribute(string groupId, string transformerId)
    {
        GroupId = groupId;
        TransformerId = transformerId;
    }
}