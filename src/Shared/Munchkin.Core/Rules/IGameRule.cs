using Munchkin.Core.Scenes;
using System.Reflection;

namespace Munchkin.Core.Rules;

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