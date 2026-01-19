using Munchkin.Entities.Actions;

namespace Munchkin.Rules.Conditions;

public class ActionRuleCondition : IRuleCondition
{
    public GameAction Action { get; }

    public ActionRuleCondition(GameAction action)
    {
        ArgumentNullException.ThrowIfNull(action);

        Action = action;
    }
}