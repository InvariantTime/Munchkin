using Example.Scenes;
using Munchkin.Core.Cards;
using Munchkin.Core.Rules;

namespace Example.Rules;

public class TakeCardRule : IGameRule
{
    public bool CanExecute(IGameRuleContext context)
    {
        return context.Scene is TakeCardScene;
    }

    public void Execute(IGameRuleContext context)
    {
        var card = context.CardPool.Pop();

        if (card.Type == CardTypes.Monster)
        {
            context.Scene = new FightScene(card.Power);
        }
        else
        {
            context.Current.Equipment.Add(card);
            ConsoleDrawer.Draw($"{context.Current.Name} takes card with power {card.Power}, new power: {context.Current.Power}", ConsoleColor.Yellow);
            context.NextPlayer();
        }
    }
}
