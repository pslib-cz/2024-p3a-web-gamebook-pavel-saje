import { useEffect, useState, useContext } from "react";
import { Location, LocationContent, Item } from "../types";

import { GameContext } from '../context/GameContext';


interface ContentProps {
    lokace: Location | null;
    }


    const Content: React.FC<ContentProps> = ({ lokace }) => {
      const [items, setItems] = useState<Item[]>([]);
      const [content, setContent] = useState<LocationContent[]>([]);
      const gameContext = useContext(GameContext);
      if (!gameContext) {
        throw new Error("GameContext is undefined");
      }
      const { inventory, setInventory } = gameContext;
      const { InteractiblesRemovedFromLocation, setInteractiblesRemovedFromLocation } = gameContext;
    
      useEffect(() => {
        const fetchData = async () => {
          try {
            const response = await fetch(`https://localhost:7092/api/LocationContent/podlelokace/${lokace?.locationID}`);
            if (!response.ok) {
              throw new Error("Failed to fetch data");
            }
            const json = await response.json();
            setContent(json);
          } catch (error) {
            console.error(error);
          }
        };
        fetchData();

        const fetchItems = async () => {
          try {
            const response = await fetch(`https://localhost:7092/api/InteractibleItems/GetInteractibleIDs/${lokace?.locationID}`);
            if (!response.ok) {
              throw new Error("Failed to fetch data");
            }
            const json = await response.json();
            setItems(json);
          } catch (error) {
            console.error(error);
          }
        };
        fetchItems();
      }, [lokace]);
    
      useEffect(() => {
        localStorage.setItem('inventory', JSON.stringify(inventory));
      }, [inventory]);

      useEffect(() => {
        localStorage.setItem('InteractiblesRemovedFromLocation', JSON.stringify(InteractiblesRemovedFromLocation));
      }, [InteractiblesRemovedFromLocation]);

      

    
      const getItemByInteractibleID = (interactibleID: number): Item | undefined => {
        console.log("item",interactibleID);
        return items.find(item => item.itemID === interactibleID);
      };

      return (
        <>

        {content &&
          content.map((contentItem, index) => {
            const key = lokace?.locationID + "-" + index;
            // const item = getItemByInteractibleID(contentItem.interactibleID);
            const item = items[0];
            console.log("pes",items[0]);
            console.log(key,"============",InteractiblesRemovedFromLocation)
            return (
              <span
                key={key}
                style={{
                  cursor: "pointer",
                  position: "absolute",
                  bottom: `${contentItem.yPos}%`,
                  left: `${contentItem.xPos}%`,
                }}
              
                onClick={() =>
                  item && (

                  setInventory((prevInventory: Item[]) => [...prevInventory, item]),
                  setInteractiblesRemovedFromLocation((prevInteractiblesRemovedFromLocation: string[]) => [...prevInteractiblesRemovedFromLocation, key]),
                  localStorage.setItem('inventory', JSON.stringify(inventory))
            )}
              >
                {item && !InteractiblesRemovedFromLocation.find((removed) => removed === key) ? (
                  <p>{item.name}</p>
                ) : (
                  null
                )}
              </span>
            );
          })}
      </>
    );
    
  }
    export default Content;