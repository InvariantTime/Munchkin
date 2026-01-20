namespace Munchkin.States.Building;

public interface IStateBuilder
{
    IStateKey Key { get; }

    IState Build();
}
