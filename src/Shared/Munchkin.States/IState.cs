namespace Munchkin.States;

public interface IState
{
    IStateKey Key { get; }

    object GetValue();
}