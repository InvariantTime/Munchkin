using Example.Scenes;
using Munchkin.Core.Rules;

namespace Example.Rules;

public class EscapeMonsterRule : IGameRule
{
    public bool CanExecute(IGameRuleContext context)
    {
        return context.Scene is FightScene && context.Action! == Actions.Fighting.Escape;
    }

    public void Execute(IGameRuleContext context)
    {
        if (context.Scene is not FightScene fight)
            return;

        var player = context.Current;

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

        context.NextPlayer();
        context.Scene = new TakeCardScene();
        context.Current.Actions.Clear();
        context.Current.Actions.Add(Actions.Common.TakeCard);
    }
}
