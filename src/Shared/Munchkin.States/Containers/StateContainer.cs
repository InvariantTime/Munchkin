using System.Collections.Frozen;

namespace Munchkin.States.Containers;

public class StateContainer : IStateContainer
{
    private readonly FrozenDictionary<IStateKey, IState> _states;

    public StateContainer(IEnumerable<IState> states)
    {
        _states = states.ToFrozenDictionary(x => x.Key);
    }

    public IState? GetState(IStateKey key)
    {
        if (key == null)
            return null;

        _states.TryGetValue(key, out var value);
        return value;
    }
}
