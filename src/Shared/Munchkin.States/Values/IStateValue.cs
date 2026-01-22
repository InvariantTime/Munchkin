using Munchkin.Notification;

namespace Munchkin.States.Values;

public interface IStateValue<out T> : INotifier<T>
{
    T Value { get; }
}
