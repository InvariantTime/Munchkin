
using Munchkin.Notification;

namespace Munchkin.States;

public interface IGenericState<T> : IState, ISourceNotifier<IGenericState<T>, T>
{
    new IGenericStateKey<T> Key { get; }

    new T Value { get; }

    bool TrySetValue(T value);
}