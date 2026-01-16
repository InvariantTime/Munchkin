

using Munchkin.Core.Scenes;
using Munchkin.Core.States;
using Munchkin.Core.States.Building;

class MyScene : GameScene
{
    public static readonly IGenericStateKey<int> MyParameterKey = 
        StateKey.Create<int>("_my_param_", string.Empty);

    public IState MyParameter => GetState(MyParameterKey)!;

    protected override void RegisterStates(IStateInitializer initializer)
    {
        initializer.RegisterState(MyParameterKey);
    }
}