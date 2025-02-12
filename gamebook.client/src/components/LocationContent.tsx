import { useEffect, useState, useContext } from "react";
import { DataLocation, InteractiblesOption} from "../types";

import { GameContext } from '../context/GameContext';

import styles from '../styles/content.module.css'

import { domain } from "../utils";

import Option from "../components/Option";

interface ContentProps {
    location: DataLocation | null;
    }


    const Content: React.FC<ContentProps> = ({ location }) => {
 const [interactiblesOptionList, setInteractiblesOptionList] = useState<InteractiblesOption[]>([]);

      const gameContext = useContext(GameContext);
      if (!gameContext) {
        throw new Error("GameContext is undefined");
      }
      const { InteractiblesRemovedFromLocation } = gameContext;
    
      useEffect(() => {
        const fetchData = async () => {
          try{
            const response = await fetch(`${domain}/api/DataInteractiblesOption`);
            const data = await response.json();
            setInteractiblesOptionList(data);
          }catch (error)
          {
            console.error("KOKOT")
          }
          }
          fetchData();
        }, [location]);

        const [mousePosition, setMousePosition] = useState({ x: 0, y: 0 });
        const [showInteractText, setShowInteractText] = useState(false);

        useEffect(() => {
          const handleMouseMove = (event: MouseEvent) => {
            setMousePosition({ x: event.clientX, y: event.clientY });
          };

          

          window.addEventListener('mousemove', handleMouseMove);

          return () => {
            window.removeEventListener('mousemove', handleMouseMove);
          };
        }, []);

        useEffect(() => {
          console.log(mousePosition);
        }, [mousePosition]);



      const getOptions = (interactibleId: number): InteractiblesOption[] | undefined => {
        const list: InteractiblesOption[] = []
        interactiblesOptionList.map((option: InteractiblesOption) => (
          option.interactibleID === interactibleId && list.push(option)
        ))
        return list
      }

      return (
        <>
          {location?.locationContents &&
            location.locationContents.map((content, index) => {
              const key = location?.locationID + "-" + index;
              const options = getOptions(content.interactibleID);
              return !InteractiblesRemovedFromLocation.find((removed) => removed === key) ?
              (
                <>
                <span
                  key={key}
                  className={styles.interactible}
                  style={{
                    position: "absolute",
                    bottom: `${content.yPos}%`,
                    left: `${content.xPos}%`,
                  }}
                  onMouseEnter={() => setShowInteractText(true)}
                  onMouseLeave={() => setShowInteractText(false)}
                >
                  <img
                    src={domain + content.interactible.imagePath}
                    alt={`${content.interactibleID}`}
                  />
                  <span>
                    <p>{content.interactible.name}</p>
                    <ul>
                      {options && options.map((option) => (
                        <Option
                        interactibleKey={key}
                          interactOption={option.option}
                          interactible={content.interactible}
                        />
                      ))}
                    </ul>
                  </span>
                </span>

                {showInteractText && (
                <p style={{position: "absolute", top: mousePosition.y, left: mousePosition.x}}>click to interact</p>
                )}
                </>
              ) : null;
            }).filter(Boolean)}
        </>
      );
    
  }
    export default Content;