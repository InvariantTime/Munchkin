using Munchkin.Scenes;

namespace Munchkin.Rules.Conditions;

public class SceneRuleCondition : IRuleCondition
{
    public Type SceneType { get; }

    public SceneRuleCondition(Type sceneType)
    {
        ArgumentNullException.ThrowIfNull(sceneType);

        if (typeof(GameScene).IsAssignableFrom(sceneType) == false)
            throw new ArgumentException("Scene type must be game scene subtype", nameof(sceneType));

        SceneType = sceneType;
    }

    public static SceneRuleCondition ForScene<T>() where T : GameScene
    {
        return new SceneRuleCondition(typeof(T));
    }
}
