using Munchkin.Core.Scenes;

namespace Example;

public class EmptyGameScene : GameScene
{
    public static readonly EmptyGameScene Instance = new();

    private EmptyGameScene()
    {
    }
}
