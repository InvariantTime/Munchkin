using Munchkin.Core.Actions;
using Munchkin.Core.Entities;
using Munchkin.Core.Scenes;

namespace Example;

public class GameEvent //TODO: interface and implementations
{
    public EventTypes Type { get; }

    public GameAction? Action { get; }

    public GameScene? GameScene { get; }

    public Player? Executor { get; }

    private GameEvent(EventTypes type, GameAction? action = null, Player? executor = null, GameScene? scene = null)
    {
        Type = type;
        Action = action;
        GameScene = scene;
        Executor = executor;
    }

    public static GameEvent ActionEvent(GameAction action, Player executor)
    {
        return new GameEvent(EventTypes.Action, action, executor);
    }

    public static GameEvent SceneEvent(GameScene scene)
    {
        return new GameEvent(EventTypes.SceneChanged, null, null, scene);
    }
}

public enum EventTypes
{
    Action,
    SceneChanged
}