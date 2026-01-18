using Munchkin.Core.States;
using Munchkin.Core.States.Building;

namespace Example;

public class StateInitializer : IStateInitializer
{
    private readonly List<IState> _states = new List<IState>();

    public void InitializeContainer(IStateContainer container)
    {
        foreach (var state in _states)
            container.AddState(state);
    }

    public IStateBuilder RegisterState(IStateKey key)
    {
        _states.Add(new SimpleState(key, key.DefaultValue));
        return new StateBuilder();
    }
}
