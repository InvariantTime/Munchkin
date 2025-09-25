using Munchkin.Core.Rules;

namespace Munchkin.Core.Actions;

public interface IGameActionAccessor
{
    IClientAccessor Executor { get; } //TODO: can executor be not a player ?

    bool HasAction { get; }

    GameAction ActionType { get; }

    object? ActionValue { get; }
}