namespace Munchkin.States;

public interface IStateAccessor
{
    IState State { get; }

    bool TrySetValue(object value);
}

public interface IStateAccessor<T> : IStateAccessor
{
    new IGenericState<T> State { get; }

    bool TrySetValue(T value);
}

public interface IStateAccessor<TValue, TSource> : IStateAccessor<TValue> where TValue : StatableObject
{
    TSource Source { get; }
}