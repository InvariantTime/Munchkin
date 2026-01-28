import { CardTypes } from "../../api/Cards/Card";
import { MiniCardView } from "./MiniCardView";
import "./CardHand.css"

const maxAngle = 35;
const maxSize = 140;

export const CardHand = () => {
    const types = [
        CardTypes.Curse,
        CardTypes.Improvement,
        CardTypes.Monster,
        CardTypes.Clothes,
        CardTypes.Curse,
        CardTypes.Improvement,
        CardTypes.Monster,
        CardTypes.Clothes,
    ];

    function calculateRotation(index: number, count: number)
    {
        if (count === 1)
            return 0;

        const angleStep = (maxAngle * 2) / (count - 1);
        const angle = -maxAngle + (angleStep * index);

        return angle;
    }

    function calculateOffset(index: number, count: number)
    {
        if (count === 1)
            return 0;

        const spacing = Math.min(maxSize, 340 / count);
        const center = (count - 1) / 2;

        return (index - center) * spacing;
    }

    return (
        <div className="handContainer">
            {types.slice(0, 5).map((type, i, array) => {

                const angle = calculateRotation(i, array.length);
                const offset = calculateOffset(i, array.length)

                const transform = `translateX(${offset}px) rotate(${angle}deg)`;

                return (
                    <div className="cardHandContainer" style={
                        {
                            transform: transform
                        }}>

                        <MiniCardView type={type} />
                    </div>
                );
            })}
        </div>
    );
};
