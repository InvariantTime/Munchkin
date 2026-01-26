import { CardBackType } from "../../api/Cards/Card";

interface Props
{
    type: CardBackType
}


export const CardBackView = ({type}: Props) =>
{
    const src = type === CardBackType.Treasure ? "assets/cards/treasureCard.png" : "assets/cards/doorCard.png";

    return (
        <div className="h-60 w-40 inline-block">
            <img src={src} className="w-full h-full"/>
        </div>
    )
}