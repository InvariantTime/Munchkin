using Munchkin.GameModel.Actions;
using System.Collections.Immutable;

namespace Munchkin.GameModel.Cards;

public class Card//TODO: make dynamic count of parameters
{
    public string Name { get; }

    public CardTypes Type { get; }

    public ImmutableArray<ICardAction> Actions { get; }

    public Card(string name, CardTypes type, IEnumerable<ICardAction> actions)
    {
        Name = name;
        Type = type;
        Actions = actions.ToImmutableArray();
    }
}