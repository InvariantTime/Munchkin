import { CardTypes, getBadgeImage } from "../../api/Cards/Card";
import "./CardStyles.css"

interface Props {
    type: CardTypes
}

export const MiniCardView = ({type}: Props) => {
    return (
        <div className="miniCardContainer">

            <div className="cardTypeBadge">
                <span>
                    <img src={getBadgeImage(type)} draggable="false"/>
                </span>
            </div>

            <div className="miniCard">
                <div className="" />

                <div className="text-2xl">Title</div>
                <div className="text-md text-neutral-700">Subtitle</div>
            </div>
        </div>
    );
};
