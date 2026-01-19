using Munchkin.Entities;

namespace Munchkin.Rules;

public interface IPlayersContext
{
    IReadOnlyList<Player> All { get; }

    Player Current { get; }

    void NextPlayer();
}
