using System.Numerics;

namespace Munchkin.Core.Parameters;

public class ParameterKey<T> : IParameterKey, IEquatable<ParameterKey<T>>, 
    IEqualityOperators<ParameterKey<T>, ParameterKey<T>, bool>
{
    public string Id { get; }

    public string DisplayName { get; }

    public ParameterKey(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    bool IEquatable<IParameterKey>.Equals(IParameterKey? other)
    {
        if (ReferenceEquals(this, other) == true)
            return true;

        if (other is not ParameterKey<T> typed)
            return false;

        return typed.Id == Id;
    }

    public bool Equals(ParameterKey<T>? other)
    {
        if (ReferenceEquals(this, other) == true)
            return true;

        if (other == null)
            return false;

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not ParameterKey<T> other)
            return false;

        return Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(ParameterKey<T>? left, ParameterKey<T>? right)
    {
        if (left is null || right is null)
            return false;

        if (ReferenceEquals(left, right) == true)
            return true;

        return left.Id == right.Id;
    }

    public static bool operator !=(ParameterKey<T>? left, ParameterKey<T>? right)
    {
        return !(left == right);
    }
}
