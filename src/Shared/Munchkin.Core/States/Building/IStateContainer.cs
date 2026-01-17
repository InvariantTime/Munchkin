namespace Munchkin.Core.States.Building;

public interface IStateContainer : IReadOnlyStateContainer
{
    void AddState(IState state);
}
