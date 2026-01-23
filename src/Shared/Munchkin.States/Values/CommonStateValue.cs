using Munchkin.Notification;
using System.Collections.Immutable;

namespace Munchkin.States.Values;

public class CommonStateValue<T> : IStateValue<T>
{
    private readonly NotifySubject<T> _notifier;
    private readonly ImmutableArray<StateValueCondition<T>> _conditions;

    public T Value { get; private set; }

    public CommonStateValue(IEnumerable<StateValueCondition<T>> conditions, T value)
    {
        _conditions = conditions.ToImmutableArray();
        _notifier = new NotifySubject<T>();
        Value = value;
    }

    public IDisposable Subscribe(INotifyListener<T> listener)
    {
        return _notifier.Subscribe(listener);
    }

    public void Dispose()
    {
        _notifier.Dispose();
    }

    private void SetValue(T value)
    {
        foreach (var condition in _conditions)
        {
            if (condition.Invoke(value) == false)
                return;
        }

        Value = value;
    }
}
