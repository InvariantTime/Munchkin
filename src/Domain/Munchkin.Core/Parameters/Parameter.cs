namespace Munchkin.Core.Parameters;

public class Parameter<T> : IParameter
{
    IParameterKey IParameter.Key => Key;

    object IParameter.Value => Value!;

    public ParameterKey<T> Key { get; }

    public T Value { get; private set; }

    public Parameter(ParameterKey<T> key, T value)
    {
        Key = key;
        Value = value;
    }

    public void Set(T value)
    {
        Value = value;
    }

    void IParameter.Set(object value)
    {
        if (value is not T typed)
            return;

        Value = typed;
    }
}