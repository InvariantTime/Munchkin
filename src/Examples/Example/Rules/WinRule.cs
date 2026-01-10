using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example.Rules;

public class WinRule : IGameRule
{
    public bool CanExecute(IGameRuleContext<GameScene> context)
    {
        return context.Players.All.Where(x => x.Level >= 10).Any() == true;
    }

    public void Execute(IGameRuleContext<GameScene> context)
    {
        var winner = context.Players.All.FirstOrDefault(x => x.Level >= 10);
        
        if (winner != null)
        {
            ConsoleDrawer.Draw($"{winner.Name} won!", ConsoleColor.Green);
            context.FinishGame();
        }
    }
}
