using Munchkin.Entities.Actions;

namespace Munchkin.Rules;

public interface IForAction
{
    GameAction TriggerAction { get; }
}