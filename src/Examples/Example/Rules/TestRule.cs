using Munchkin.Core.Actions;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example.Rules;

public class TestRule : IGameRule, IForAction
{
    public GameAction TriggerAction => Actions.Common.TakeCard;

    public void Execute(IGameRuleContext<GameScene> context)
    {
        Console.WriteLine($"rule completed by {context.Action.Executor?.Name ?? string.Empty}");
    }
}
