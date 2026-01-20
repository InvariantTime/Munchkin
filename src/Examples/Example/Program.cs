using Munchkin.Scenes;
using Munchkin.States;
using Munchkin.States.Building;
using Munchkin.States.Containers;

/*

void Rule()
{
    

}

*/


class FightScene : GameScene
{
    public static readonly IGenericStateKey<bool> CanFightKey = StateKey.Create<bool>("_can_fight_state_", "Can fight", false);

    public IGenericState<bool> CanFight => States.GetRequiredState(CanFightKey);

    protected override void InitializeStates(IStateInitializer initializer)
    {
        initializer.RegisterState(CanFightKey);
    }
}