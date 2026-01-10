using Example.Scenes;
using Munchkin.Core.Actions;
using Munchkin.Core.Cards;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example.Rules;

public class TakeCardRule : IGameRule, IForAction
{
    public GameAction TriggerAction => Actions.Common.TakeCard;

    public void Execute(IGameRuleContext<GameScene> context)
    {
        var card = context.CardPool.Pop();

        if (card.Type == CardTypes.Monster)
        {
            context.Scene.SetScene(new FightScene(card.Power));
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
