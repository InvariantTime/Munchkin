namespace Munchkin.Utils.Parameters;

public interface IGameParameter
{
    IGameParameterKey Key { get; }

    object? Value { get; }
}

public interface IGameParameter<T> : IGameParameter
{
    new GameParameterKey<T> Key { get; }

    new T? Value { get; }
}