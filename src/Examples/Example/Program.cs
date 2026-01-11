using Example;
using Example.Rules;
using Example.Scenes;
using Munchkin.Core.Cards;
using Munchkin.Core.Entities;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;

Console.WriteLine(typeof(GameScene).IsAssignableFrom(typeof(TakeCardScene)));

IGameRuleBase[] rules = [

    new TakeCardInitialRule(),
    new TakeCardRule(),
    new EscapeMonsterRule(),
    new AttackMonsterRule(),
    new WinRule()
];

Player[] players = [

    new Player("Petya"),
    new Player("Vasya"),
    new Player("Taras")
];

var cards = Enumerable.Empty<Card>();

var state = new GameState(players, new TakeCardScene(), cards);
GameRuleLauncher launcher = new GameRuleLauncher(rules);

launcher.Launch(state, GameEvent.ActionEvent(Actions.Common.TakeCard, state.Players.Current));