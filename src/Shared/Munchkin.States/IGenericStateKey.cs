namespace Munchkin.States;

public interface IGenericStateKey<T> : IStateKey, IEquatable<IGenericStateKey<T>>
{
    Type IStateKey.ValueType => typeof(T);

    new T? DefaultValue { get; }
}
