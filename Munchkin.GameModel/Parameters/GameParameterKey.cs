using CSharpFunctionalExtensions;

namespace Munchkin.GameModel.Parameters;

public sealed class GameParameterKey<T> : ValueObject, IGameParameterKey
{
    public string Name { get; }

    public Type Type => typeof(T);

    public T? DefaultValue { get; }

    object? IGameParameterKey.DefaultValue => DefaultValue;

    public GameParameterKey(string name, T? defaultValue = default)
    {
        Name = name;
        DefaultValue = defaultValue;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Name;
        yield return typeof(T);
    }
}