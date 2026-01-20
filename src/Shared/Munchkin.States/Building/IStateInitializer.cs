using Munchkin.States.Containers;

namespace Munchkin.States.Building;

public interface IStateInitializer
{
    IStateBuilder RegisterState(IStateKey key);

    IStateBuilder<T> RegisterState<T>(IGenericStateKey<T> key);

    IStateContainer BuildContainer();
}