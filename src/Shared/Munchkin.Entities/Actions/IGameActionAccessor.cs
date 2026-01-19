
namespace Munchkin.Entities.Actions;

public interface IGameActionAccessor
{
    GameAction Action { get; }

    bool HasAction { get; }

    Player? Executor { get; }
}
