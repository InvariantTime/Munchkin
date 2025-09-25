namespace Munchkin.Core.Parameters;

public interface IParameterKey : IEquatable<IParameterKey>
{
    string Id { get; }

    string DisplayName { get; }
}