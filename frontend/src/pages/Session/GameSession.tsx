import { CardBackType, CardTypes } from "../../api/Cards/Card";
import { CardBackView } from "../../components/Card/CardBackView";
import { HandDemo } from "../../components/Card/CardExample";
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
                    <MiniCardView type={CardTypes.Monster}/>
                    <MiniCardView type={CardTypes.Clothes}/>
                    <MiniCardView type={CardTypes.Curse}/>
                    <MiniCardView type={CardTypes.Improvement}/>
                    <CardBackView type={CardBackType.Door}/>
                    <CardBackView type={CardBackType.Treasure}/>
                </div>
            </div>
        </div>
    );
}