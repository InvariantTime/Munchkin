namespace Munchkin.Core.Parameters;

public interface IParameter
{
    IParameterKey Key { get; }

    object Value { get; }

    void Set(object value);
}