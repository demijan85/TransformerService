using System.Reflection;
using Transformer.Api.Attributes;
using Transformer.Api.Services.Interfaces;

namespace Transformer.Api.Services.Implementations;

public class TransformerFactoryScoped : ITransformerFactory
{
    private readonly IServiceProvider _serviceProvider;
    private readonly Dictionary<(string, string), Type> _transformerTypes;

    public TransformerFactoryScoped(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _transformerTypes = new Dictionary<(string, string), Type>();

        LoadTransformers();
    }

    private void LoadTransformers()
    {
        var transformerInterface = typeof(ITransformer);
        var types = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => transformerInterface.IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

        foreach (var type in types)
        {
            var attr = type.GetCustomAttribute<TransformerAttribute>();
            if (attr != null)
            {
                _transformerTypes[(attr.GroupId, attr.TransformerId)] = type;
            }
        }
    }

    public ITransformer GetTransformer(string groupId, string transformerId)
    {
        if (!_transformerTypes.TryGetValue((groupId, transformerId), out var type))
            throw new InvalidOperationException(
                $"Transformer not found for GroupId: {groupId}, TransformerId: {transformerId}");
        
        var transformer = (ITransformer)_serviceProvider.GetService(type)!;
        
        if (transformer == null)
        {
            throw new InvalidOperationException($"Failed to resolve transformer of type {type.FullName}");
        }
        
        return transformer;
    }
}