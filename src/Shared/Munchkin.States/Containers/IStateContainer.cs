namespace Munchkin.States.Containers;

public interface IStateContainer
{
    IState? GetState(IStateKey key);
}
