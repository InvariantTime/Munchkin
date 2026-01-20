namespace Munchkin.States.Containers;

public static partial class StateContainerExtensions
{
    public static IGenericState<T>? GetState<T>(this IStateContainer container, IGenericStateKey<T> key)
        where T : notnull
    {
        return GetState(container, key);
    }

    public static IGenericState<T>? GetState<T>(this IStateContainer container, IStateKey key)
        where T : notnull
    {
        var state = container.GetState(key);

        if (state is not IGenericState<T> generic)
            return null;

        return generic;
    }

    public static IGenericState<T> GetRequiredState<T>(this IStateContainer container, IStateKey key)
        where T : notnull
    {
        var state = GetState<T>(container, key);

        if (state == null)
            throw new InvalidOperationException($"Unable to get state with key {key.Id}");

        return state;
    }

    public static IGenericState<T> GetRequiredState<T>(this IStateContainer container, IGenericStateKey<T> key)
        where T : notnull
    {
        var state = GetState(container, key);

        if (state == null)
            throw new InvalidOperationException($"Unable to get state with key {key.Id}");

        return state;
    }
}
