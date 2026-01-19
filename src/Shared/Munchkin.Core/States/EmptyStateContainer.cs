using Munchkin.Core.States.Building;
using System.ComponentModel;

namespace Munchkin.Core.States;

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
