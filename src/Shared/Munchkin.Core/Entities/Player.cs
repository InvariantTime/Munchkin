using Munchkin.Core.Actions;
using Munchkin.Core.Cards;

namespace Munchkin.Core.Entities;

public class Player
{
    public string Name { get; }

    public List<Card> Hand { get; } = new();

    public List<Card> Equipment { get; } = new();

    public List<GameAction> Actions { get; } = new();

    public int Level { get; private set; } = 1;

    public int Power => Equipment.Sum(x => x.Power) + Level;

    public Player(string name)
    {
        Name = name;
    }

    public void LevelUp()
    {
        Level++;
    }

    public void Kill()
    {
        Level = 1;
    }
}
