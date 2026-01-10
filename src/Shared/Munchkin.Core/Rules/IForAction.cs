using Munchkin.Core.Actions;

namespace Munchkin.Core.Rules;

public interface IForAction
{
    GameAction TriggerAction { get; }
}
