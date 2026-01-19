using Munchkin.Core.States.Building;

namespace Munchkin.Core.States;

public abstract class StatableObject : IStatable
{
    private IStateContainer? _states;

    public IStateContainer States => _states ?? EmptyStateContainer.Instance;

    public void OnInitialize(IStateInitializer initializer)
    {
        InitializeStates(initializer);
        _states = initializer.BuildContainer();
    }

    protected virtual void InitializeStates(IStateInitializer initializer)
    {
    }
}
