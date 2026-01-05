import { ReactNode } from "react";
import "./Background.css"

interface Props {
  children?: React.ReactNode
}


export const Background = ({children} : Props) => {
  return (
    <div className="background">
      <div className="munchkin"/>
        <div className="w-full z-10">
          {children}
        </div>
    </div>
  );
};