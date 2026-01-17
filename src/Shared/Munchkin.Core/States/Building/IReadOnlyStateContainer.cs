namespace Munchkin.Core.States.Building;

public interface IReadOnlyStateContainer
{
    IState? GetState(IStateKey key);
}
