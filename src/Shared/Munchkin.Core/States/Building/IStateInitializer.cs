namespace Munchkin.Core.States.Building;

public interface IStateInitializer
{
    IStateBuilder RegisterState(IStateKey key);
}