using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example;

public class GameRuleContext : IGameRuleContext
{
    private int _index = 0;

    public GameScene Scene 
    {
        get => field ?? EmptyGameScene.Instance;
        
        set => field = value;
    }

    public IReadOnlyCollection<Player> Players { get; }

    public Player Current => Players.ElementAt(_index);

    public Stack<Card> CardPool { get; }

    public bool IsRunning { get; set; } = true;

    public GameRuleContext(IEnumerable<Player> players, IEnumerable<Card> pool, GameScene startScene)
    {
        Scene = startScene;
        Players = players.ToArray();
        CardPool = new Stack<Card>(pool);
    }

    public void NextPlayer()
    {
        _index = _index + 1 < Players.Count ? _index + 1 : 0;
    }
}
