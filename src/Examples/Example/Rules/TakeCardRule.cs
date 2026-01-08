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
            context.Current.Actions.Clear();
            context.Current.Actions.Add(Actions.Fighting.Attack);
            context.Current.Actions.Add(Actions.Fighting.Escape);
        }
        else
        {
            context.NextPlayer();
            context.Current.Actions.Clear();
            context.Current.Actions.Add(Actions.Common.TakeCard);
        }
    }
}
