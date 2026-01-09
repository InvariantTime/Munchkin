using Example.Scenes;
using Munchkin.Core.Rules;

namespace Example.Rules;

public class AttackMonsterRule : IGameRule
{
    public bool CanExecute(IGameRuleContext context)
    {
        return context.Scene is FightScene && context.Action! == Actions.Fighting.Attack;
    }

    public void Execute(IGameRuleContext context)
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
        context.Scene = new TakeCardScene();
        context.Players.Current.Actions.Clear();
        context.Players.Current.Actions.Add(Actions.Common.TakeCard);
    }
}
