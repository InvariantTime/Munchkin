namespace Munchkin.Core.Rules;

public interface IExecutable
{
    void Execute(IGameRuleContext context);
}