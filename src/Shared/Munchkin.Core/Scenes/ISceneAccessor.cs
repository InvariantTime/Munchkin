namespace Munchkin.Core.Scenes;

public interface ISceneAccessor<out T>
{
    T Scene { get; }

    void SetScene(GameScene scene);
}
