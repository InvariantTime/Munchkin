namespace Munchkin.Entities.Actions;

public class GameAction : IEquatable<GameAction>
{
    public static readonly GameAction EmptyAction = new("_empty_action_", "None");

    public string Id { get; }

    public string DisplayName { get; }

    public GameAction(string id, string displayName)
    {
        Id = id;
        DisplayName = displayName;
    }

    public bool Equals(GameAction? other)
    {
        if (other is null)
            return false;

        if (ReferenceEquals(other, this) == true)
            return true;

        return Id == other.Id;
    }

    public override bool Equals(object? obj)
    {
        return Equals(obj as GameAction);
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }

    public static bool operator ==(GameAction? left, GameAction? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null)
            return false;

        return left.Equals(right);
    }

    public static bool operator !=(GameAction? left, GameAction? right)
    {
        return !(left == right);
    }
}
