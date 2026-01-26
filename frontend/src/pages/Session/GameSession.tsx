import { CardBackType } from "../../api/Cards/Card";
import { CardBackView } from "../../components/Card/CardBackView";
import { CardView } from "../../components/Card/CardView";
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
                    <CardView/>
                    <CardBackView type={CardBackType.Door}/>
                    <CardBackView type={CardBackType.Treasure}/>
                </div>
            </div>
        </div>
    );
}