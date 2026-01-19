namespace Munchkin.States;

public class GenericStateKey<T> : IGenericStateKey<T>
{
    public string Id { get; }

    public string DisplayName { get; }

    public T? DefaultValue { get; }

    object? IStateKey.DefaultValue => DefaultValue;

    public GenericStateKey(string id, string? displayName = null, T? defaultValue = default)
    {
        Id = id;
        DisplayName = displayName ?? string.Empty;
        DefaultValue = defaultValue;
    }

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(this, obj) == true)
            return true;

        if (obj is not IGenericStateKey<T> generic)
            return false;

        return Equals(generic);
    }

    public bool Equals(IStateKey? other)
    {
        if (other == null)
            return false;

        if (other is not IGenericStateKey<T> generic)
            return false;

        return Equals(generic);
    }

    public bool Equals(IGenericStateKey<T>? other)
    {
        if (other == null)
            return false;

        return Id == other.Id;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(typeof(T), Id);
    }
}
