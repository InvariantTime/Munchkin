using Example;
using Munchkin.Core.Scenes;
using Munchkin.Core.States;
using Munchkin.Core.States.Building;

var initializer = new StateInitializer();
MyScene scene = new MyScene();
scene.OnInitialize(initializer);

var state = scene.States.GetState(MyScene.MyParameterKey);

Console.WriteLine();

class MyScene : GameScene
{
    public static readonly IGenericStateKey<int> MyParameterKey = 
        StateKey.Create("_my_param_", "My parameter", 100);

    public IState MyParameter => States.GetState(MyParameterKey)!;

    protected override void InitializeStates(IStateInitializer initializer)
    {
        initializer.RegisterState(MyParameterKey);
    }
}