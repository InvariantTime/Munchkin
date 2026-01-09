using Example.Scenes;
using Munchkin.Core.Cards;
using Munchkin.Core.Rules;

namespace Example.Rules;

public class TakeCardRule : IGameRule
{
    public bool CanExecute(IGameRuleContext context)
    {
        return context.Scene is TakeCardScene && context.Action! == Actions.Common.TakeCard;
    }

    public void Execute(IGameRuleContext context)
    {
        var card = context.CardPool.Pop();

        if (card.Type == CardTypes.Monster)
        {
            context.Scene = new FightScene(card.Power);
            context.Players.Current.Actions.Clear();
            context.Players.Current.Actions.Add(Actions.Fighting.Attack);
            context.Players.Current.Actions.Add(Actions.Fighting.Escape);
        }
        else
        {
            context.Players.NextPlayer();
            context.Players.Current.Actions.Clear();
            context.Players.Current.Actions.Add(Actions.Common.TakeCard);
        }
    }
}
