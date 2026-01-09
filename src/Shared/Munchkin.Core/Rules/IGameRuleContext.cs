using Munchkin.Core.Actions;
using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Scenes;

namespace Munchkin.Core.Rules;

public interface IGameRuleContext<T> where T : GameScene
{
    T Scene { get; set; }

    IPlayersContext Players { get; }

    Stack<Card> CardPool { get; }

    bool IsRunning { get; set; }

    public GameAction? Action { get; set; }
}

public interface IGameRuleContext : IGameRuleContext<GameScene>
{
}