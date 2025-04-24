namespace Munchkin.GameModel.Parameters;

public interface IGameParameterContainer
{
    IReadOnlyDictionary<IGameParameterKey, IGameParameter> Parameters { get; }

    IGameParameter? GetParameter(IGameParameterKey key);
}