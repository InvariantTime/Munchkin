using Example;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

var players = new Player[]
{
    new Player("Petya"),
    new Player("Vasya"),
    new Player("Taras")
};

var rules = new IGameRule[]
{
    new TakeCardRule(),
    new StartFightRule(),
    new WinRule()
};

var cards = GenerateCards(150);
Game game = new Game(players, cards);

while (game.IsRunning == true)
{
    var execRules = rules.Where(x => x.CanExecute(game) == true);

    foreach (var rule in execRules)
        rule.Execute(game);
}


IEnumerable<Card> GenerateCards(int count)
{
    var cards = new List<Card>();

    for (int i = 0; i < count; i++)
    {
        var type = (CardTypes)Random.Shared.Next(2);
        int power = 0;

        if (type == CardTypes.Clothes)
            power = Random.Shared.Next(1, 5);
        else if (type == CardTypes.Monster)
            power = Random.Shared.Next(5, 30);

        var card = new Card($"Card {i}", type, power);
        cards.Add(card);
    }

    return cards;
}

class Game
{
    private readonly Stack<Card> _pool;
    private readonly ImmutableArray<Player> _players;

    private int _index = 0;

    public Player Player => _players[_index];

    public IState State { get; set; } = new TakeCardState();

    public bool IsRunning { get; private set; } = true;

    public Game(IEnumerable<Player> players, IEnumerable<Card> cards)
    {
        _pool = new Stack<Card>(cards);
        _players = players.ToImmutableArray();

        foreach (var player in _players)
        {
            for (int i = 0; i < 6; i++)
                player.AddCard(_pool.Pop());
        }
    }

    public void NextPlayer()
    {
        _index = _index + 1 < _players.Length ? _index + 1 : 0;
    }

    public Card TakeCard()
    {
        var card = _pool.Pop();
        return card;
    }

    public void PutCard(Card card)
    {
        _pool.Push(card);
    }

    public Player? GetWinner()
    {
        return _players.FirstOrDefault(x => x.Level >= 10);
    }

    public void End()
    {
        IsRunning = false;
    }
}

class Player
{
    private readonly List<Card> _cards;

    public IReadOnlyCollection<Card> Cards { get; }

    public int Power => _cards.Where(x => x.Type == CardTypes.Clothes).Sum(x => x.Power);

    public int Level { get; private set; } = 1;

    public string Name { get; }

    public Player(string name)
    {
        _cards = new List<Card>();
        Name = name;
        Cards = new ReadOnlyCollection<Card>(_cards);
    }

    public void AddCard(Card card)
    {
        _cards.Add(card);
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

class Card
{
    public CardTypes Type { get; }

    public string Name { get; }

    public int Power { get; }

    public Card(string name, CardTypes type, int power)
    {
        Type = type;
        Name = name;
        Power = power;
    }
}

enum CardTypes
{
    Clothes,
    Monster,
}

interface IGameRule
{
    bool CanExecute(Game game);

    void Execute(Game game);
}

class TakeCardRule : IGameRule
{
    public bool CanExecute(Game game)
    {
        return game.State is TakeCardState;
    }

    public void Execute(Game game)
    {
        var card = game.TakeCard();

        ConsoleDrawer.Draw($"Take Card: {game.Player.Name}, CardType: {card.Type}", ConsoleColor.Yellow);

        if (card.Type == CardTypes.Monster)
        {
            game.State = new MonsterCardState(card.Power);
        }
        else if (card.Type == CardTypes.Clothes)
        {
            game.Player.AddCard(card);
            game.NextPlayer();
        }
    }
}

class StartFightRule : IGameRule
{
    public bool CanExecute(Game game)
    {
        return game.State is MonsterCardState;
    }

    public void Execute(Game game)
    {
        if (game.State is not MonsterCardState monster)
            return;

        ConsoleDrawer.Draw($"Fight: Fighter: [{game.Player.Name}, Power: {game.Player.Power}], Monster Power: {monster.Power}", ConsoleColor.Yellow);

        var player = game.Player;

        if (player.Power < monster.Power)
        {
            player.Kill();
            ConsoleDrawer.Draw($"{player.Name} lost in battle", ConsoleColor.Red);
        }
        else
        {
            player.LevelUp();
            ConsoleDrawer.Draw($"{player.Name} won in battle", ConsoleColor.Blue);
        }

        game.NextPlayer();
        game.State = new TakeCardState();
    }
}

class WinRule : IGameRule
{
    public bool CanExecute(Game game)
    {
        return game.GetWinner() != null;
    }

    public void Execute(Game game)
    {
        var winner = game.GetWinner();

        if (winner == null)
            return;

        game.End();
        ConsoleDrawer.Draw($"{winner.Name} won", ConsoleColor.Green);
    }
}

interface IState
{
}

class TakeCardState : IState
{
}

class MonsterCardState : IState
{
    public int Power { get; }

    public MonsterCardState(int power)
    {
        Power = power;
    }
}