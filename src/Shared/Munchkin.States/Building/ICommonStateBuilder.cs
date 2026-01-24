
namespace Munchkin.States.Building;

public interface ICommonStateBuilder<T> : IStateBuilder
{
    new IGenericStateKey<T> Key { get; }

    ICommonStateBuilder<T> AddCondition(StateValueCondition<T> condition);
}
