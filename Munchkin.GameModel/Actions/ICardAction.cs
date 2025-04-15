using Munchkin.GameModel.Scenes;

namespace Munchkin.GameModel.Actions;

public interface ICardAction
{
    void Execute(GameScene scene);
}