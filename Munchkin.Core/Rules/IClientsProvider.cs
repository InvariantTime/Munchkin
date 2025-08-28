namespace Munchkin.Core.Rules;

public interface IClientsProvider
{
    IClientContext Current { get; }

    IClientContext Others { get; }

    IClientContext All { get; }
}
