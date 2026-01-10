using Munchkin.Core.Scenes;

namespace Example;

public class GameSceneAccessor<T> : ISceneAccessor<T> where T : GameScene
{
    private readonly GameState _state;

    public T Scene { get; }

    public GameSceneAccessor(T scene, GameState state)
    {
        Scene = scene;
        _state = state;
    }

    public void SetScene(GameScene scene)
    {
        _state.SetScene(scene);
    }
}
