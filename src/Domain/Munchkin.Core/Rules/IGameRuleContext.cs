using Munchkin.Core.Actions;
using Munchkin.Core.Scenes;
using System.Diagnostics.CodeAnalysis;

namespace Munchkin.Core.Rules;

public interface IGameRuleContext
{
    IClientsProvider Clients { get; }

    IGameActionAccessor Action { get; }

    bool TryGet<TParam>([NotNullWhen(true)]out TParam? result);
}

public interface IGameRuleContext<T> : IGameRuleContext
    where T : IGameScene
{
    IGameSceneAccessor<T> Scenes { get; }
}