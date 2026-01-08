using Munchkin.Core.Actions;
using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Scenes;

namespace Munchkin.Core.Rules;

public interface IGameRuleContext<T> where T : GameScene
{
    T Scene { get; set; }

    IReadOnlyCollection<Player> Players { get; }

    Player Current { get; }

    GameAction? Action { get;  set; }

    Stack<Card> CardPool { get; }

    bool IsRunning { get; set; }

    void NextPlayer();
}

public interface IGameRuleContext : IGameRuleContext<GameScene>
{
}