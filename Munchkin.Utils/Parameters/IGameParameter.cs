namespace Munchkin.Utils.Parameters;

public interface IGameParameter
{
    IGameParameterKey Key { get; }

    object? Value { get; }
}

public interface IGameParameter<T> : IGameParameter
{
    GameParameterKey<T> Key { get; }

    T? Value { get; }
}