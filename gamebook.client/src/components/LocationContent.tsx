import { useEffect, useState, useContext } from "react";
import { DataLocation, InteractiblesItem, InteractiblesOption, Item, InteractOption, Interactible} from "../types";

import { GameContext } from '../context/GameContext';

import styles from '../styles/content.module.css'

import { domain } from "../utils";

interface ContentProps {
    location: DataLocation | null;
    }


    const Content: React.FC<ContentProps> = ({ location }) => {
      const [interactiblesItems, setInteractiblesItems] = useState<InteractiblesItem[]>([]);
      const [interactiblesOptionList, setInteractiblesOptionList] = useState<InteractiblesOption[]>([]);

      const gameContext = useContext(GameContext);
      if (!gameContext) {
        throw new Error("GameContext is undefined");
      }
      // const { inventory, setInventory } = gameContext;
      const { InteractiblesRemovedFromLocation, setInteractiblesRemovedFromLocation } = gameContext;
    
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
        });

      useEffect(() => {
        const fetchData = async () => {
          if (location) {
            try {
              const response = await fetch(`${domain}/api/DataInteractiblesItem`);
              const data = await response.json();
              setInteractiblesItems(data);
            } catch (error) {
              console.error("Error fetching interactibles:", error);
            }
          }
        };

        fetchData();
      }, [location]);

      const isItem = (interactibleId: number): InteractiblesItem | undefined => {
        return interactiblesItems.find(intItem => intItem.interactibleID === interactibleId);
      }

      const getOptions = (interactibleId: number): InteractiblesOption[] | undefined => {
        const list: InteractiblesOption[] = []
        interactiblesOptionList.map((option: InteractiblesOption) => (
          option.interactibleID === interactibleId && list.push(option)
        ))
        return list
      }

      console.log(location?.locationContents)

      return (
        <>
          {location?.locationContents &&
            location.locationContents.map((content, index) => {
              const interactible = isItem(content.interactibleID);
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
                  onClick={() =>
                    // interactible && (
        
                    // setInventory((prevInventory: Item[]) => [...prevInventory, interactible.item]),
                    // setInteractiblesRemovedFromLocation((prevInteractiblesRemovedFromLocation: string[]) => [...prevInteractiblesRemovedFromLocation, key]),
                    // localStorage.setItem('inventory', JSON.stringify(inventory))
                    
                    options && alert(options.map((option) => option.option.optionText))
              // )
            }
                >
                  <img
                    src={domain + content.interactible.imagePath}
                    alt={`${content.interactibleID}`}
                  />
                  <span>
                    <p>{content.interactible.name}</p>
                    <ul>
                      
                    </ul>
                  </span>
                </span>
                </>
              ) : null;
            }).filter(Boolean)}
        </>
      );
    
  }
    export default Content;