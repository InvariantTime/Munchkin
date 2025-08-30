using Munchkin.Core.Actions;
using Munchkin.Core.Scenes;
using System.Diagnostics.CodeAnalysis;

namespace Munchkin.Core.Rules;

public interface IGameRuleContext<T> where T : IGameScene
{
    IClientsProvider Clients { get; }

    IGameSceneAccessor<T> Scenes { get; }

    IGameActionAccessor Action { get; }

    bool TryGet<TParam>([NotNullWhen(true)]out TParam? result);
}

public interface IGameRuleContext : IGameRuleContext<IGameScene>
{
}
