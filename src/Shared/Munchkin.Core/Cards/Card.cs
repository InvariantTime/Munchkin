namespace Munchkin.Core.Cards;

public class Card
{
    public string Name { get; }

    public CardTypes Type { get; }

    public int Power { get; }

    public Card(string name, int power, CardTypes type)
    {
        Name = name;
        Power = power;
        Type = type;
    }
}

public enum CardTypes
{
    Equipment,
    Monster
}