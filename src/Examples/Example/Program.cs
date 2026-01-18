

using Munchkin.Core.Scenes;
using Munchkin.Core.States;
using Munchkin.Core.States.Building;

MyScene scene = new MyScene();

var state = scene.GetState(MyScene.MyParameterKey);

Console.WriteLine();

class MyScene : GameScene
{
    public static readonly IGenericStateKey<int> MyParameterKey = 
        StateKey.Create("_my_param_", string.Empty, 100);

    public IState MyParameter => GetState(MyParameterKey)!;

    protected override void RegisterStates(IStateInitializer initializer)
    {
        initializer.RegisterState(MyParameterKey);
    }
}