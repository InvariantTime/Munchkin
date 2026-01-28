import { CardBackType, CardTypes } from "../../api/Cards/Card";
import { CardBackView } from "../../components/Card/CardBackView";
import CardHandExm from "../../components/Card/CardExample";
import { CardHand } from "../../components/Card/CardHand";
import { MiniCardView } from "../../components/Card/MiniCardView";
import "./GameSession.css"

export const GameSession = () => {

    return (
        <div className="h-full">
            <div className="container">
                <div className="playerList">

                </div>

                 <div className="scene">

                </div>

                 <div className="handArea">
                    <CardHand/>
                </div>
            </div>
        </div>
    );
}