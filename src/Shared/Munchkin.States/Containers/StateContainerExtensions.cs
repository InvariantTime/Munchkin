namespace Munchkin.States.Containers;

public static partial class StateContainerExtensions
{
    public static IState GetRequiredState(this IStateContainer container, IStateKey key)
    {
        var state = container.GetState(key);

        if (state == null)
            throw new InvalidOperationException($"Unable to get state with key {key.Id}");

        return state;
    }
}
