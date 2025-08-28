namespace Munchkin.Core.Actions;

public class GameAction : IEquatable<GameAction>
{
    public string Id { get; }

    public string DisplayName { get; }

    public GameAction(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public bool Equals(GameAction? other)
    {
        if (other == null)
            return false;

        return other.Id == Id;
    }

    public override bool Equals(object? obj)
    {
        if (obj is not GameAction other)
            return false;

        return Equals(other);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}