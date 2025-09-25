using Munchkin.Core.Actions;

namespace Munchkin.Core.Rules;

public interface IClientAccessor
{
    IClientAccessor AddAction(GameAction action);

    IClientAccessor SavePrevActions();

    IClientAccessor RemovePrevActions();
}
