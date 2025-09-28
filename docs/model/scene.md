## Сцена (GameScene)

Модель `GameScene` представляет собой контейнер, который инкапсулирует полное состояние игры в конкретный момент времени. В любое время может быть только одна сцена

### Пример

К примеру игрок вытаскивает карту с монстром, что запускает сцену, описывающую битву с этим монстром:

```csharp

class BattleScene
{
    public int MonsterPower { get; }

    public Card MonsterCard { get; }

    public bool CanEscape { get; }

    public IModifier[] Modifiers { get; }

    public BattleStep { get; }
}
// Это всё псевдокод для примера
 ```

 Здесь мы видим, что класс `BattleScene` описывает всё состояние боя.