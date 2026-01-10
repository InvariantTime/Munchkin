using Example.Scenes;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example.Rules;

public class TakeCardInitialRule : IGameRule<TakeCardScene>
{

    public void Execute(IGameRuleContext<TakeCardScene> context)
    {
        context.Players.Current.Actions.Add(Actions.Common.TakeCard);
    }
}
