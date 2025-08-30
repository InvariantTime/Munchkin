using Munchkin.Core.Actions;

namespace Munchkin.Core.Rules;

public interface IOnAction
{
    GameAction TriggeredAction { get; }
}
