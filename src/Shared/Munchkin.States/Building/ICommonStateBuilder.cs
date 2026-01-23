using Munchkin.States.Values;

namespace Munchkin.States.Building;

public interface ICommonStateBuilder<T> : IStateBuilder
{
    new IGenericStateKey<T> Key { get; }

    ICommonStateBuilder<T> AddCondition(StateValueCondition<T> condition);

    ICommonStateBuilder<T> AsCalculatable(ICalculatableBuilder<T> builder);
}
