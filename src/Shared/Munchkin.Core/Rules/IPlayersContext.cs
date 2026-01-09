using Munchkin.Core.Entities;

namespace Munchkin.Core.Rules;

public interface IPlayersContext
{
    IReadOnlyList<Player> All { get; }

    Player Current { get; }

    void NextPlayer();
}
