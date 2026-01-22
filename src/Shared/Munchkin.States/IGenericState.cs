using Munchkin.States.Values;

namespace Munchkin.States;

public interface IGenericState<T> : IState
{
    new IGenericStateKey<T> Key { get; }

    new IStateValue<T> Value { get; }
}