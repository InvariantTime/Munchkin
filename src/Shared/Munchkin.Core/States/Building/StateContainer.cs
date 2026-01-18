namespace Munchkin.Core.States.Building;

public class StateContainer : IStateContainer
{
    private readonly Dictionary<IStateKey, IState> _states;

    public StateContainer()
    {
        _states = new Dictionary<IStateKey, IState>();
    }

    public void AddState(IState state)
    {
        _states.Add(state.Key, state);
    }

    public IState? GetState(IStateKey key)
    {
        if (key == null)
            return null;

        _states.TryGetValue(key, out var value);
        return value;
    }
}
