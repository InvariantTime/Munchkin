using Example.Players;
using Munchkin.Core.Actions;
using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

namespace Example;

public class GameRuleContext<T> : IGameRuleContext<T> where T : GameScene
{
    private readonly GameState _state;

    public T Scene { get; }

    public Stack<Card> CardPool => _state.Pool;

    public GameAction? Action { get; set; }

    public IPlayersContext Players => _state.Players;

    public GameRuleContext(T scene, GameState state)
    {
        Scene = scene;
        _state = state;
    }

    public void SetScene(GameScene scene)
    {
        _state.SetScene(scene);
    }

    public void FinishGame()
    {
        _state.Finish();
    }
}
