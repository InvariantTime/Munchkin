using Munchkin.Core.Scenes;

namespace Munchkin.Core.Rules;

public interface IGameRuleBase
{
    bool CanExecute(IGameRuleContext context);
}

public interface IGameRule : IGameRuleBase
{
    void Execute(IGameRuleContext context);
}

public interface IGameRule<T> : IGameRuleBase where T : GameScene
{
    void Execute(IGameRuleContext<T> context);
}