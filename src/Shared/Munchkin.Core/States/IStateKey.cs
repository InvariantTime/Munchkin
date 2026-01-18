namespace Munchkin.Core.States;

public interface IStateKey : IEquatable<IStateKey>
{
    string Id { get; }

    string DisplayName { get; }

    Type ValueType { get; }

    object? DefaultValue { get; }
}