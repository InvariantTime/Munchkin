using Example.Scenes;
using Munchkin.Core.Actions;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example.Rules;

public class EscapeMonsterRule : IGameRule, IForAction
{
    public GameAction TriggerAction => Actions.Fighting.Escape;

    public void Execute(IGameRuleContext<GameScene> context)
    {
        if (context.Scene is not FightScene fight)
            return;

        var player = context.Players.Current;

        ConsoleDrawer.Draw($"{player.Name} try escape", ConsoleColor.Yellow);
        int chance = Random.Shared.Next(1, 11);

        if (chance < 7)
        {
            ConsoleDrawer.Draw($"{player.Name} couldn't escape", ConsoleColor.Red);
            player.Kill();
        }
        else
        {
            ConsoleDrawer.Draw($"{player.Name} escaped", ConsoleColor.Blue);
            player.LevelUp();
        }

        context.Players.NextPlayer();
        context.SetScene(new TakeCardScene());
        context.Players.Current.Actions.Clear();
        context.Players.Current.Actions.Add(Actions.Common.TakeCard);
    }
}
