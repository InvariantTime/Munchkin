using System.Collections.Immutable;

namespace Munchkin.Core.Parameters;

public class ParameterContainer : IParameterContainer
{
    private readonly ImmutableDictionary<IParameterKey, IParameter> _parameters;

    public IEnumerable<IParameter> Parameters => _parameters.Values;

    public ParameterContainer(IEnumerable<IParameter> parameters)
    {
        _parameters = parameters.ToImmutableDictionary(x => x.Key);
    }


    public bool ContainsKey(IParameterKey key)
    {
        return _parameters.ContainsKey(key);
    }

    public IParameter? GetParameter(IParameterKey key)
    {
        return _parameters.GetValueOrDefault(key);
    }
}
