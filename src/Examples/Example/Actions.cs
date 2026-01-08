using Munchkin.Core.Actions;

namespace Example;

public static class Actions
{
    public static class Common
    {
        public static readonly GameAction TakeCard = new("_take_card_", "Вытащить карту");
    }

    public static class Fighting
    {
        public static readonly GameAction Attack = new("_attack_monster", "Атаковать");

        public static readonly GameAction Escape = new("_escape_monster_", "Убежать");
    }
}
