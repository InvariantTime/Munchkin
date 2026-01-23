namespace Munchkin.States.Building;

public static class StateInitializerExtensions
{
    public static ICommonStateBuilder<T> RegisterCommonState<T>(this IStateInitializer initializer, IGenericStateKey<T> key)
    {
        return initializer.Register<ICommonStateBuilder<T>>(key);
    }
}
