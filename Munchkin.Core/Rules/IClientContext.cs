using Munchkin.Core.Actions;

namespace Munchkin.Core.Rules;

public interface IClientContext
{
    IClientContext AddAction(GameAction action);

    IClientContext SavePrevActions();

    IClientContext RemovePrevActions();
}
