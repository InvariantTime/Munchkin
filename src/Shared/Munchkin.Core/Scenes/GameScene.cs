using Munchkin.Core.States;
using Munchkin.Core.States.Building;

namespace Munchkin.Core.Scenes;

public abstract class GameScene : IStatable
{
    public void OnInitialize(IStateInitializer initializer)
    {
        //TODO: base initializing
        RegisterStates(initializer);
    }

    public IState? GetState(IStateKey key)
    {
        return null!;
    }

    protected abstract void RegisterStates(IStateInitializer initializer);
}
