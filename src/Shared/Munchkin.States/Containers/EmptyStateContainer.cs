namespace Munchkin.States.Containers;

public class EmptyStateContainer : IStateContainer
{
    public static readonly EmptyStateContainer Instance = new();

    private EmptyStateContainer()
    {
    }

    public IState? GetState(IStateKey key)
    {
        return null;
    }
}
