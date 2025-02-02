import { useEffect, useState, useContext } from "react";
import { DataLocation, LocationContent, Item } from "../types";

import { GameContext } from '../context/GameContext';
import { Position } from "react-flow-renderer";

import styles from '../styles/content.module.css'

import { domain } from "../utils";

interface ContentProps {
    location: DataLocation | null;
    }


    const Content: React.FC<ContentProps> = ({ location }) => {
      // const [items, setItems] = useState<Item[]>([]);
      // const gameContext = useContext(GameContext);
      // if (!gameContext) {
      //   throw new Error("GameContext is undefined");
      // }
      // const { inventory, setInventory } = gameContext;
      // const { InteractiblesRemovedFromLocation, setInteractiblesRemovedFromLocation } = gameContext;
    
      // useEffect(() => {
      //   localStorage.setItem('inventory', JSON.stringify(inventory));
      // }, [inventory]);

      // useEffect(() => {
      //   localStorage.setItem('InteractiblesRemovedFromLocation', JSON.stringify(InteractiblesRemovedFromLocation));
      // }, [InteractiblesRemovedFromLocation]);

      

    
      // const getItemByInteractibleID = (interactibleID: number): Item | undefined => {
      //   console.log("item",interactibleID);
      //   return items.find(item => item.itemID === interactibleID);
      // };

      console.log(location?.locationContents)

      return (
        <>
          {location?.locationContents &&
            location.locationContents.map((content, index) => (
              <span className={styles.interactible} style={{
                position: "absolute",
                bottom: `${content.yPos}%`,
                left: `${content.xPos}%`,
              }}>
                {/* KOKOT{content.interactible.name} */}
                <img style={{
                  width: "7rem",
                  aspectRatio: 1
                }} src={domain + content.interactible.imagePath} alt={`${content.interactibleID}`}/>
              </span>
            ))}

          {/* {location?.locationContents &&
          location.locationContents.map((contentItem, index) => {
            const key = location?.locationID + "-" + index;
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
          })} */}
        </>
      );
    
  }
    export default Content;