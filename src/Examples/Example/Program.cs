using Example;
using Example.Rules;
using Example.Scenes;
using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Rules;
using System.Collections.Immutable;

IGameRule[] rules = [
    
    new TakeCardRule(),
    new StartFightRule(),
    new WinRule()
];

Player[] players = [

    new Player("Petya"),
    new Player("Vasya"),
    new Player("Taras")
];

var cards = GenerateCards(150);

var game = new Game(rules, new GameRuleContext(players, cards, new TakeCardScene()));
game.Run();

IEnumerable<Card> GenerateCards(int count)
{
    List<Card> cards = new();

    for (int i = 0; i < count; i++)
    {
        var type = Random.Shared.Next(0, 10) < 4 ? CardTypes.Equipment : CardTypes.Monster;
        int power = 0;

        if (type == CardTypes.Monster)
            power = Random.Shared.Next(1, 30);
        else
            power = Random.Shared.Next(1, 6);

        cards.Add(new Card($"card {i}", power, type));
    }

    return cards;
}


class Game
{
    private readonly ImmutableArray<IGameRule> _rules;
    private readonly IGameRuleContext _context;

    public Game(IEnumerable<IGameRule> rules, IGameRuleContext context)
    {
        _rules = rules.ToImmutableArray();
        _context = context;
    }

    public void Run()
    {
        while (_context.IsRunning == true)
        {
            var activeRules = _rules.Where(x => x.CanExecute(_context) == true);

            if (activeRules.Any() == false)
                return;

            foreach (var rule in activeRules)
                rule.Execute(_context);
        }

        ConsoleDrawer.Draw("Win!", ConsoleColor.Green);
    }
}

