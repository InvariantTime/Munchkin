using Munchkin.Core.Actions;
using Munchkin.Core.Rules;
using Munchkin.Core.Rules.Conditions;
using Munchkin.Core.Scenes;
using System.Collections.Immutable;

namespace Example;

public class GameRuleDescription
{
    public IGameRuleBase Rule { get; }

    public ImmutableArray<IRuleCondition> Conditions { get; }

    private GameRuleDescription(IGameRuleBase rule, IEnumerable<IRuleCondition> conditions)
    {
        Rule = rule;
        Conditions = conditions.ToImmutableArray();
    }

    public bool ForScene(GameScene scene)
    {
        var sceneType = scene.GetType();

        return Conditions.Where(x => x is SceneRuleCondition)
            .FirstOrDefault(x => (x as SceneRuleCondition)!.SceneType == sceneType) != null;
    }

    public bool ForAction(GameAction action)
    {
        return Conditions.Where(x => x is ActionRuleCondition)
            .FirstOrDefault(x => (x as ActionRuleCondition)!.Action == action) != null;
    }

    public static GameRuleDescription Create(IGameRuleBase rule)
    {
        List<IRuleCondition> conditions = new();

        if (rule is IForAction action)
            conditions.Add(new ActionRuleCondition(action.TriggerAction));

        var scene = GetSceneType(rule);

        if (scene != null)
            conditions.Add(new SceneRuleCondition(scene));

        return new GameRuleDescription(rule, conditions);
    }

    private static Type? GetSceneType(IGameRuleBase rule)
    {
        var type = rule.GetType().GetInterfaces()
            .FirstOrDefault(x => x.IsGenericType == true && x.GetGenericTypeDefinition() == typeof(IGameRule<>));

        if (type == null)
            return null;

        var scene = type.GetGenericArguments().Single();

        return scene;
    }
}
