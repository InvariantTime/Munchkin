using Munchkin.Core.Scenes;

namespace Example.Scenes;

public class FightScene : GameScene
{
    public int Power { get; }

    public FightScene(int power)
    {
        Power = power;
    }
}
