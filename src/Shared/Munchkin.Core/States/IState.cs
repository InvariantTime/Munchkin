namespace Munchkin.Core.States;

public interface IState
{
    IStateKey Key { get; }

    object GetValue();
}