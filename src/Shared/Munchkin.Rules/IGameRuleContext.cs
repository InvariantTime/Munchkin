using Munchkin.Cards;
using Munchkin.Entities.Actions;
using Munchkin.Scenes;

namespace Munchkin.Rules;

public interface IGameRuleContext<out T> where T : GameScene
{
    ISceneAccessor<T> Scene { get; }

    IPlayersContext Players { get; }

    Stack<Card> CardPool { get; }

    public IGameActionAccessor Action { get; }

    void FinishGame();
}