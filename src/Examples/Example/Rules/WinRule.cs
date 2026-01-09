using Munchkin.Core.Rules;

namespace Example.Rules;

public class WinRule : IGameRule
{
    public bool CanExecute(IGameRuleContext context)
    {
        return context.Players.All.Where(x => x.Level >= 10).Any() == true;
    }

    public void Execute(IGameRuleContext context)
    {
        var winner = context.Players.All.FirstOrDefault(x => x.Level >= 10);
        
        if (winner != null)
        {
            ConsoleDrawer.Draw($"{winner.Name} won!", ConsoleColor.Green);
            context.IsRunning = false;
        }
    }
}
