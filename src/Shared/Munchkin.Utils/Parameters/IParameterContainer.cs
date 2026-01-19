namespace Munchkin.Utils.Parameters;

public interface IParameterContainer
{
    IEnumerable<IParameter> Parameters { get; }

    bool ContainsKey(IParameterKey key);

    IParameter? GetParameter(IParameterKey key);
}
