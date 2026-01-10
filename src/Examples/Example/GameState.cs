using Example.Players;
using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;
using System.Reflection;

namespace Example;

public class GameState
{
    public static readonly PropertyInfo SceneProperty = 
        typeof(GameState).GetProperty(nameof(CurrentScene), BindingFlags.Public | BindingFlags.Instance)!;

    public IPlayersContext Players { get; }

    public GameScene? CurrentScene { get; private set; }

    public Stack<Card> Pool { get; }

    public bool IsRunning { get; private set; } = true;

    public GameState(IEnumerable<Player> players, GameScene startScene, IEnumerable<Card> cards)
    {
        Players = new PlayersContext(players);
        CurrentScene = startScene;
        Pool = new Stack<Card>(cards);
    }

    public void SetScene(GameScene scene)
    {
        CurrentScene = scene;
    }

    public void Finish()
    {
        IsRunning = false;
    }
}
