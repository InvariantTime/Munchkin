export type Card =
{

}

export enum CardBackType
{
    Treasure,
    Door
}

export enum CardTypes
{
    Monster,
    Curse,
    Clothes,
    Improvement
}

export function getBadgeImage(type: CardTypes) : string
{
    switch (type)
    {
        case CardTypes.Curse: return "assets/cards/types/curse.png"
        case CardTypes.Improvement: return "assets/cards/types/improvement.png"
        case CardTypes.Clothes: return "assets/cards/types/clothes.png"
        case CardTypes.Monster: return "assets/cards/types/monster.png"
    }
}