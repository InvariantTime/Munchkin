namespace Munchkin.GameModel.Parameters;

public class ImmutableGameParameter<T> : IGameParameter<T>
{
    public GameParameterKey<T> Key { get; }

    public T? Value { get; }

    IGameParameterKey IGameParameter.Key => Key;

    object? IGameParameter.Value => Value;

    public ImmutableGameParameter(GameParameterKey<T> key, T? objectValue = default)
    {
        Key = key;
        Value = objectValue ?? Key.DefaultValue;
    }
}