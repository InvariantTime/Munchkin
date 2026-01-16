namespace Munchkin.Core.States;

public interface IGenericStateKey<T> : IStateKey
{
    Type IStateKey.ValueType => typeof(T);
}
