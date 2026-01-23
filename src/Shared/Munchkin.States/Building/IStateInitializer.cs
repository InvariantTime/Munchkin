using Munchkin.States.Containers;

namespace Munchkin.States.Building;

public interface IStateInitializer
{
    T Register<T>(IStateKey key) where T : IStateBuilder;

    IStateContainer BuildContainer();
}