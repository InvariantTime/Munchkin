namespace Munchkin.Core.Scenes;

public interface IGameSceneAccessor<T> where T : IGameScene
{
    T Current { get; }

    void SetCurrent(IGameScene scene);
}