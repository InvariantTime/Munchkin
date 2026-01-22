using Munchkin.States.Values;

namespace Munchkin.States;

public interface IState
{
    IStateKey Key { get; }

    IStateValue<object> Value { get; }
}