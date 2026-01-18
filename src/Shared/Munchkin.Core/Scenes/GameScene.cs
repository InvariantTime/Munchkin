using Munchkin.Core.States;
using Munchkin.Core.States.Building;

namespace Munchkin.Core.Scenes;

public abstract class GameScene : IStatable
{
    private readonly IStateContainer _states;

    protected GameScene()
    {
        _states = new StateContainer();
    }

    public void OnInitialize(IStateInitializer initializer)
    {
        RegisterStates(initializer);
        initializer.InitializeContainer(_states);
    }

    public IState? GetState(IStateKey key)
    {
        return _states.GetState(key);
    }

    protected abstract void RegisterStates(IStateInitializer initializer);
}
