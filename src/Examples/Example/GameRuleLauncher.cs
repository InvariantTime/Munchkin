using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;

namespace Example;

public class GameRuleLauncher
{
    private readonly ConcurrentDictionary<Type, Action<IGameRuleBase, GameState>> _ruleExecutors;
    private readonly ImmutableArray<IGameRuleBase> _rules;

    public GameRuleLauncher(IEnumerable<IGameRuleBase> rules)
    {
        _rules = rules.ToImmutableArray();
        _ruleExecutors = new ConcurrentDictionary<Type, Action<IGameRuleBase, GameState>>();
    }

    public bool Launch(GameState state, GameEvent @event)
    {
        var scene = state.CurrentScene;

        if (scene == null)
            return false;

        var rules = _rules.Where(x => CanExecute(x, scene.GetType()) == true);

        if (rules.Any() == false)
            return false;

        foreach (var rule in rules)
            TryCallRule(rule, state);

        return true;
    }

    private void TryCallRule(IGameRuleBase rule, GameState state)
    {
        if (state.CurrentScene == null)
            return;

        var type = GetSceneType(rule);

        if (type == null)
            return;

        var executor = _ruleExecutors.GetOrAdd(type, CreateExecutor);
        executor.Invoke(rule, state);
    }

    private bool CanExecute(IGameRuleBase rule, Type sceneType)
    {
        if (rule is IGameRule)
            return true;

        if (rule is IGameRule<GameScene>)
            return true;

        var type = GetSceneType(rule);

        if (type == null)
            return false;

        return type.IsAssignableFrom(sceneType);
    }

    private Type? GetSceneType(IGameRuleBase rule)
    {
        if (rule is IGameRule)
            return typeof(GameScene);

        if (rule is IGameRule<GameScene>)
            return typeof(GameScene);

        var ruleInterface = rule.GetType().GetInterfaces()
            .Where(x => x.IsGenericType && x.GetGenericTypeDefinition() == typeof(IGameRule<>))
            .FirstOrDefault();

        if (ruleInterface == null)
            return null;

        var generic = ruleInterface.GetGenericArguments().First();
        return generic;
    }

    private Action<IGameRuleBase, GameState> CreateExecutor(Type sceneType)
    {
        var ruleParameter = Expression.Parameter(typeof(IGameRuleBase));
        var gameStateParameter = Expression.Parameter(typeof(GameState));

        var ctor = typeof(GameRuleContext<>)
            .MakeGenericType(sceneType)
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
            .Single();

        var sceneParameter = Expression.Property(gameStateParameter, GameState.SceneProperty);
        var sceneParameterConverted = Expression.Convert(sceneParameter, sceneType);

        var @new = Expression.New(ctor, sceneParameterConverted, gameStateParameter);

        var canExecute = Expression.Call(ruleParameter, IGameRuleBase.CanExecuteMethod, 
            Expression.Convert(@new, typeof(IGameRuleContext<GameScene>)));

        var checkRuleType = Expression.TypeIs(ruleParameter, typeof(IGameRule));

        var typedContext = typeof(IGameRuleContext<>).MakeGenericType(sceneType);

        var executeScenelessExpression = Expression.Call(Expression.Convert(ruleParameter, typeof(IGameRule)), 
            IGameRule.ExecuteMethod, Expression.Convert(@new, typeof(IGameRuleContext<GameScene>)));

        var scenedRuleType = typeof(IGameRule<>).MakeGenericType(sceneType);

        var ruleExecuteMethod = scenedRuleType
            .GetMethod("Execute", BindingFlags.Public | BindingFlags.Instance)!;

        var executeScenedExpression = Expression.Call(Expression.Convert(ruleParameter, scenedRuleType), 
            ruleExecuteMethod, Expression.Convert(@new, typedContext));

        var ruleIsSceneless = Expression.IfThenElse(checkRuleType, executeScenelessExpression, executeScenedExpression);

        var body = Expression.IfThen(canExecute, ruleIsSceneless);

        var lambda = Expression.Lambda<Action<IGameRuleBase, GameState>>(body, ruleParameter, gameStateParameter);
        return lambda.Compile();
    }
}
