using Munchkin.Core.Actions;
using Munchkin.Core.Scenes;

namespace Example;

public class GameEvent //TODO: interface and implementations
{
    public EventTypes Type { get; }

    public GameAction? Action { get; }

    public GameScene? GameScene { get; }

    private GameEvent(EventTypes type, GameAction? action = null, GameScene? scene = null)
    {
        Type = type;
        Action = action;
        GameScene = scene;
    }

    public static GameEvent ActionEvent(GameAction action)
    {
        return new GameEvent(EventTypes.Action, action);
    }

    public static GameEvent SceneEvent(GameScene scene)
    {
        return new GameEvent(EventTypes.SceneChanged, null, scene);
    }
}

public enum EventTypes
{
    Action,
    SceneChanged
}