namespace Munchkin.Utils.Parameters;

public class GameParameter<T> : IGameParameter<T>
{
    public GameParameterKey<T> Key { get; }

    public T? Value { get; private set; }

    object? IGameParameter.Value => Value;

    IGameParameterKey IGameParameter.Key => Key;

    public GameParameter(GameParameterKey<T> key, T? value = default)
    {
        Key = key;
        Value = value ?? key.DefaultValue;
    }

    public void SetValue(T value)
    {
        Value = value;
    }
}