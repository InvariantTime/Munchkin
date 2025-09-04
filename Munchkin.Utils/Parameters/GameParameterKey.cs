namespace Munchkin.Utils.Parameters;

public sealed class GameParameterKey<T> : IEquatable<GameParameterKey<T>>, IGameParameterKey
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

    public bool Equals(GameParameterKey<T>? other)
    {
        if (other == null)
            return false;

        return Name == other.Name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not GameParameterKey<T> other)
            return false;

        return Equals(other);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Name, Type);
    }
}