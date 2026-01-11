using Munchkin.Core.Actions;
using Munchkin.Core.Entities;

namespace Example;

public class EmptyActionAccessor : IGameActionAccessor
{
    public static readonly EmptyActionAccessor Instance = new();

    public GameAction Action => GameAction.EmptyAction;

    public bool HasAction => false;

    public Player? Executor => null;

    private EmptyActionAccessor()
    {
    }
}
