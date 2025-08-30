namespace Munchkin.Core.Rules;

public interface IClientsProvider
{
    IClientContext Executor { get; }

    IClientContext Current { get; }

    IClientContext Others { get; }

    IClientContext All { get; }
}
