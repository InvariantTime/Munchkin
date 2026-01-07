using Example.Scenes;
using Munchkin.Core.Rules;

namespace Example.Rules;

public class StartFightRule : IGameRule
{
    public bool CanExecute(IGameRuleContext context)
    {
        return context.Scene is FightScene;
    }

    public void Execute(IGameRuleContext context)
    {
        if (context.Scene is not FightScene fight)
            return;

        var player = context.Current;

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

        context.NextPlayer();
        context.Scene = new TakeCardScene();
    }
}
