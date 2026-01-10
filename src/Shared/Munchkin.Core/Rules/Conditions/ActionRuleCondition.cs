using Munchkin.Core.Actions;

namespace Munchkin.Core.Rules.Conditions;

public class ActionRuleCondition : IRuleCondition
{
    public GameAction Action { get; }

    public ActionRuleCondition(GameAction action)
    {
        ArgumentNullException.ThrowIfNull(action);

        Action = action;
    }
}