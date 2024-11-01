﻿using System.Reflection;
using Transformer.Api.Attributes;
using Transformer.Api.Services.Interfaces;

namespace Transformer.Api.Services.Implementations;

public class TransformerFactory : ITransformerFactory
{
    private readonly Dictionary<(string, string), ITransformer> _transformerInstances;

    public TransformerFactory(IServiceProvider serviceProvider)
    {
        _transformerInstances = new Dictionary<(string, string), ITransformer>();

        LoadTransformers(serviceProvider);
    }

    private void LoadTransformers(IServiceProvider serviceProvider)
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
                var transformerInstance = (ITransformer)serviceProvider.GetRequiredService(type);
                _transformerInstances[(attr.GroupId, attr.TransformerId)] = transformerInstance;
            }
        }
    }

    public ITransformer GetTransformer(string groupId, string transformerId)
    {
        if (!_transformerInstances.TryGetValue((groupId, transformerId), out var transformer))
            throw new InvalidOperationException(
                $"Transformer not found for GroupId: {groupId}, TransformerId: {transformerId}");

        return transformer;
    }
}