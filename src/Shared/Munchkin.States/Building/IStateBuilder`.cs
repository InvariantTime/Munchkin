namespace Munchkin.States.Building;

public interface IStateBuilder<T> : IStateBuilder
{
    new IGenericStateKey<T> Key { get; }

    
}
