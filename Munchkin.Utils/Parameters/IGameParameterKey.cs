namespace Munchkin.Utils.Parameters;

public interface IGameParameterKey
{
    string Name { get; }

    Type Type { get; }

    object? DefaultValue { get; }
}