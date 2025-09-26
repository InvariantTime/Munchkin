using Munchkin.Core.Entities;
using System.Numerics;

namespace Munchkin.Core.Parameters;

public class EntityParameterKey<TEntity, T> : IParameterKey, 
    IEquatable<EntityParameterKey<TEntity, T>>,
    IEqualityOperators<EntityParameterKey<TEntity, T>, EntityParameterKey<TEntity, T>, bool>
    where TEntity : IEntity
{
    public string Id { get; }

    public string DisplayName { get; }

    public EntityParameterKey(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    bool IEquatable<IParameterKey>.Equals(IParameterKey? other)
    {
        if (other is not EntityParameterKey<TEntity, T> typed)
            return false;

        return Equals(typed);
    }

    public bool Equals(EntityParameterKey<TEntity, T>? other)
    {
        if (ReferenceEquals(this, other) == true)
            return true;

        if (other is null)
            return false;

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not EntityParameterKey<TEntity, T> typed)
            return false;

        return Equals(typed);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(EntityParameterKey<TEntity, T>? left, EntityParameterKey<TEntity, T>? right)
    {
        if (left is null || right is null)
            return false;

        if (ReferenceEquals(left, right) == true)
            return true;

        return left.Id == right.Id;
    }

    public static bool operator !=(EntityParameterKey<TEntity, T>? left, EntityParameterKey<TEntity, T>? right)
    {
        return !(left == right);
    }
}
