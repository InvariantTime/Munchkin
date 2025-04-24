
using System.Collections.Immutable;

namespace Munchkin.GameModel.Parameters;

public class GameParameterContainer : IGameParameterContainer
{
    private readonly ImmutableDictionary<IGameParameterKey, IGameParameter> _parameters;

    public IReadOnlyDictionary<IGameParameterKey, IGameParameter> Parameters => _parameters;

    public GameParameterContainer(IDictionary<IGameParameterKey, IGameParameter> parameters)
    {
        _parameters = parameters.ToImmutableDictionary();
    }

    public IGameParameter? GetParameter(IGameParameterKey key)
    {
        _parameters.TryGetValue(key, out var value);
        return value;
    }
}