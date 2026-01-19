namespace Munchkin.Utils.Parameters;

public interface IParameter
{
    IParameterKey Key { get; }

    object Value { get; }
}
