using Munchkin.Core.Scenes;

namespace Munchkin.Core.Rules;

public interface IOnScene<T> where T : IGameScene
{
    void Execute(IGameRuleContext<T> context);
}