using Example.Players;
using Munchkin.Core.Actions;
using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example;

public class GameRuleContext : IGameRuleContext
{
    public GameScene Scene 
    {
        get => field ?? EmptyGameScene.Instance;
        
        set => field = value;
    }

    public Stack<Card> CardPool { get; }

    public bool IsRunning { get; set; } = true;

    public GameAction? Action { get; set; }

    public IPlayersContext Players { get; }

    public GameRuleContext(IEnumerable<Player> players, IEnumerable<Card> pool, GameScene startScene)
    {
        Scene = startScene;
        CardPool = new Stack<Card>(pool);
        Players = new PlayersContext(players);
    }
}
