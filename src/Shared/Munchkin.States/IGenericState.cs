namespace Munchkin.States;

public interface IGenericState<T> : IState where T : notnull
{
    new IGenericStateKey<T> Key { get; }

    new T GetValue();
}