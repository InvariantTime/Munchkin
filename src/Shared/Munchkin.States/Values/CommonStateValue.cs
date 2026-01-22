using Munchkin.Notification;
using System.Collections.Immutable;

namespace Munchkin.States.Values;

public class CommonStateValue<T> : IStateValue<T>
{
    private readonly ImmutableArray<StateValueCondition<T>> _conditions;

    private T _value;

    public T Value => _value;

    public CommonStateValue(IEnumerable<StateValueCondition<T>> conditions, T value)
    {
        _conditions = conditions.ToImmutableArray();
        _value = value;
    }

    public IDisposable Subscribe(INotifyListener<T> listener)
    {
        throw new NotImplementedException();
    }

    public void Dispose()
    {
        throw new NotImplementedException();
    }

    private void SetValue(T value)
    {
        foreach (var condition in _conditions)
        {
            if (condition.Invoke(value) == false)
                return;
        }

        _value = value;
    }
}
