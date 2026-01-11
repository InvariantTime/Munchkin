using Munchkin.Core.Actions;
using Munchkin.Core.Entities;

namespace Example;

public class ActionAccessor : IGameActionAccessor
{
    public GameAction Action { get; }

    public bool HasAction => Action != null;

    public Player? Executor { get; }

    public ActionAccessor(GameAction action, Player player)
    {
        Action = action;
        Executor = player;
    }
}
