using Munchkin.Core.Actions;
using Munchkin.Core.Cards;
using Munchkin.Core.Scenes;

namespace Munchkin.Core.Rules;

public interface IGameRuleContext<out T> where T : GameScene
{
    ISceneAccessor<T> Scene { get; }

    IPlayersContext Players { get; }

    Stack<Card> CardPool { get; }

    public GameAction? Action { get; set; }

    void FinishGame();
}