namespace Munchkin.States;

public interface IStateContext
{
    IStateAccessor? GetState(IStateKey key);
}
