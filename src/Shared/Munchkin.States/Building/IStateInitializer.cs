using Munchkin.States.Containers;

namespace Munchkin.States.Building;

public interface IStateInitializer
{
    IStateBuilder RegisterState(IStateKey key);

    IStateContainer BuildContainer();
}