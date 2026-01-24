using Munchkin.Notification;

namespace Munchkin.States;

public interface IState : ISourceNotifier<IState, object>
{
    IStateKey Key { get; }

    object Value { get; }

    bool TrySetValue(object value);
}