using Munchkin.Core.States;
using Munchkin.Core.States.Building;

namespace Munchkin.Core.Scenes;

public abstract class GameScene : IStatable
{
    private readonly IStateContainer _states;

    public void OnInitialize(IStateInitializer initializer)
    {
        //TODO: base initializing
        var containerInitializer = initializer.RegisterContainer(_states);
        RegisterStates(containerInitializer);
    }

    public IState? GetState(IStateKey key)
    {
        return _states.GetState(key);
    }

    protected abstract void RegisterStates(IStateInitializer initializer);
}
