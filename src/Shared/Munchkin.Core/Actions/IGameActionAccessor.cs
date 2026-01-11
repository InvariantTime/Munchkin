using Munchkin.Core.Entities;

namespace Munchkin.Core.Actions;

public interface IGameActionAccessor
{
    GameAction Action { get; }

    bool HasAction { get; }

    Player? Executor { get; }
}
