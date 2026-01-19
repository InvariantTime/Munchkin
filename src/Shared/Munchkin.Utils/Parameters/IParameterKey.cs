namespace Munchkin.Utils.Parameters;

public interface IParameterKey : IEquatable<IParameterKey>
{
    string Id { get; }

    string DisplayName { get; }

    object? DefaultValue { get; }
}