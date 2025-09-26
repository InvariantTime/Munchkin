using Munchkin.Core.Entities;
using System.Numerics;

namespace Munchkin.Core.Parameters;

public class EntityParameterKey<TEntity, T> : IParameterKey, 
    IEquatable<EntityParameterKey<TEntity, T>>
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

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}
