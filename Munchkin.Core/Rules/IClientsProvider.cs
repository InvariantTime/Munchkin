namespace Munchkin.Core.Rules;

public interface IClientsProvider
{
    IClientAccessor Current { get; }

    IClientAccessor Others { get; }

    IClientAccessor All { get; }
}
