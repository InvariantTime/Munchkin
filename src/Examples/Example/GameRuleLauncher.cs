using Munchkin.Core.Rules;
using System.Collections.Immutable;

namespace Example;

public class GameRuleLauncher
{
    private readonly ImmutableArray<IGameRuleBase> _rules;

    public GameRuleLauncher(IEnumerable<IGameRuleBase> rules)
    {
        _rules = rules.ToImmutableArray();
    }

    public void Launch()
    {

    }
}
