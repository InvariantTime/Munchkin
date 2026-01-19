using Munchkin.Scenes;
using Munchkin.States;
using Munchkin.States.Building;
using Munchkin.States.Containers;

var initializer = new StateInitializer();

var scene = new MyScene();
scene.OnInitialize(initializer);

var parameter = scene.MyParameter;
Console.WriteLine(parameter.GetValue());

class MyScene : GameScene
{
    public static readonly IGenericStateKey<int> MyParameterKey = StateKey.Create<int>("my_param", "My parameter", 100);

    public IState MyParameter => States.GetState(MyParameterKey)!;

    protected override void InitializeStates(IStateInitializer initializer)
    {
        initializer.RegisterState(MyParameterKey);
    }
}

class StateInitializer : IStateInitializer
{
    private readonly List<IState> _states = new();

    public IStateContainer BuildContainer()
    {
        return new StateContainer(_states);
    }

    public IStateBuilder RegisterState(IStateKey key)
    {
        _states.Add(new SimpleState(key));
        return null!;
    }
}

class SimpleState : IState
{
    public IStateKey Key { get; }

    public SimpleState(IStateKey key)
    {
        Key = key;
    }

    public object GetValue()
    {
        return Key.DefaultValue ?? new object();
    }
}