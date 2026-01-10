using Munchkin.Core.Scenes;
using System.Reflection;

namespace Munchkin.Core.Rules;

public interface IGameRuleBase
{
    public static readonly MethodInfo CanExecuteMethod = 
        typeof(IGameRuleBase).GetMethod(nameof(CanExecute), BindingFlags.Public | BindingFlags.Instance)!;

    bool CanExecute(IGameRuleContext<GameScene> context);
}

public interface IGameRule : IGameRuleBase
{
    public static readonly MethodInfo ExecuteMethod = 
        typeof(IGameRule).GetMethod(nameof(Execute), BindingFlags.Public | BindingFlags.Instance)!;

    void Execute(IGameRuleContext<GameScene> context);
}

public interface IGameRule<T> : IGameRuleBase where T : GameScene
{
    void Execute(IGameRuleContext<T> context);
}