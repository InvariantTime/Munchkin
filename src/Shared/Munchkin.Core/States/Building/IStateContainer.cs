namespace Munchkin.Core.States.Building;

public interface IStateContainer
{
    IState? GetState(IStateKey key);
}
