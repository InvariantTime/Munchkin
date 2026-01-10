using Example.Scenes;
using Munchkin.Core.Actions;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example.Rules;

public class AttackMonsterRule : IGameRule, IForAction
{
    public GameAction TriggerAction => Actions.Fighting.Attack;

    public void Execute(IGameRuleContext<GameScene> context)
    {
        if (context.Scene is not FightScene fight)
            return;

        var player = context.Players.Current;

        ConsoleDrawer.Draw($"{player.Name} starts fight", ConsoleColor.Yellow);

        if (player.Power < fight.Power)
        {
            ConsoleDrawer.Draw($"{player.Name} lost", ConsoleColor.Red);
            player.Kill();
        }
        else
        {
            ConsoleDrawer.Draw($"{player.Name} won in fight", ConsoleColor.Blue);
            player.LevelUp();
        }

        context.Players.NextPlayer();
        context.SetScene(new TakeCardScene());
        context.Players.Current.Actions.Clear();
        context.Players.Current.Actions.Add(Actions.Common.TakeCard);
    }
}
