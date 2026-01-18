using Munchkin.Core.States;

namespace Example;

public class SimpleState : IState
{
    private static readonly object _empty = new();

    public IStateKey Key { get; }

    public object Value { get; }

    public SimpleState(IStateKey key, object? value)
    {
        Key = key;
        Value = value ?? _empty;
    }

    public object GetValue()
    {
        return Value;
    }
}
