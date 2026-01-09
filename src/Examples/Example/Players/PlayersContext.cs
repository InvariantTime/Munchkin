using Munchkin.Core.Entities;
using Munchkin.Core.Rules;
using System.Collections.Immutable;
using System.Collections.ObjectModel;

namespace Example.Players;

public class PlayersContext : IPlayersContext
{
    private int _playerIndex;

    public IReadOnlyList<Player> All { get; }

    public Player Current => All[_playerIndex];

    public PlayersContext(IEnumerable<Player> players)
    {
        All = players.ToImmutableArray();
    }

    public void NextPlayer()
    {
        _playerIndex = _playerIndex + 1 < All.Count ? _playerIndex + 1 : 0;
    }
}
