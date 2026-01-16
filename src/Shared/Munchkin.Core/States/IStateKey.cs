namespace Munchkin.Core.States;

public interface IStateKey
{
    string Id { get; }

    string DisplayName { get; }

    Type ValueType { get; }
}