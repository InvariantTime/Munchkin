using Munchkin.Core.Actions;
using Munchkin.Core.Rules;
using Munchkin.Core.Scenes;
using System.Collections.Concurrent;
using System.Collections.Immutable;
using System.Linq.Expressions;
using System.Reflection;

namespace Example;

public class GameRuleLauncher
{
    private readonly ConcurrentDictionary<Type, Action<IGameRuleBase, GameState, IGameActionAccessor>> _ruleExecutors;
    private readonly ImmutableArray<GameRuleDescription> _rules;

    public GameRuleLauncher(IEnumerable<IGameRuleBase> rules)
    {
        _rules = rules.Select(GameRuleDescription.Create).ToImmutableArray();
        _ruleExecutors = new ConcurrentDictionary<Type, Action<IGameRuleBase, GameState, IGameActionAccessor>>();
    }

    public void Launch(GameState state, GameEvent @event)
    {
        if (@event.Type == EventTypes.SceneChanged)
        {
            if (@event.GameScene == null)
                return;

            var rules = _rules.Where(x => x.ForScene(@event.GameScene))
                .Where(x => x.Conditions.Length == 1);

            foreach (var rule in rules)
                TryCallRule(rule.Rule, state, EmptyActionAccessor.Instance);
        }
        else if (@event.Type == EventTypes.Action)
        {
            if (@event.Action == null)
                return;

            if (@event.Executor == null)
                return;

            var rules = _rules.Where(x => x.ForAction(@event.Action))
                .Where(x => x.Conditions.Length == 1);

            foreach (var rule in rules)
                TryCallRule(rule.Rule, state, new ActionAccessor(@event.Action, @event.Executor));
        }
    }

    private void TryCallRule(IGameRuleBase rule, GameState state, IGameActionAccessor action)
    {
        if (state.CurrentScene == null)
            return;

        var type = GetSceneType(rule);

        if (type == null)
            return;

        var executor = _ruleExecutors.GetOrAdd(type, CreateExecutor);
        executor.Invoke(rule, state, action);
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

    private Action<IGameRuleBase, GameState, IGameActionAccessor> CreateExecutor(Type sceneType)
    {
        var ruleParameter = Expression.Parameter(typeof(IGameRuleBase));
        var gameStateParameter = Expression.Parameter(typeof(GameState));
        var actionParameter = Expression.Parameter(typeof(IGameActionAccessor));

        var ctor = typeof(GameRuleContext<>)
            .MakeGenericType(sceneType)
            .GetConstructors(BindingFlags.Instance | BindingFlags.Public)
            .Single();

        var sceneParameter = Expression.Property(gameStateParameter, GameState.SceneProperty);
        var sceneParameterConverted = Expression.Convert(sceneParameter, sceneType);

        var @new = Expression.New(ctor, sceneParameterConverted, Expression.Convert(actionParameter, typeof(IGameActionAccessor)), gameStateParameter);

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

        var lambda = Expression.Lambda<Action<IGameRuleBase, GameState, IGameActionAccessor>>(ruleIsSceneless, ruleParameter, gameStateParameter, actionParameter);
        return lambda.Compile();
    }
}
