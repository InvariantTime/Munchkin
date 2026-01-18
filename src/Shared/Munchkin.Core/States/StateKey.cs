namespace Munchkin.Core.States;

public static partial class StateKey
{
    public static IGenericStateKey<T> Create<T>(string id, string displayName, T? defaultValue = default)
    {
        return new GenericStateKey<T>(id, displayName, defaultValue);
    }
}
