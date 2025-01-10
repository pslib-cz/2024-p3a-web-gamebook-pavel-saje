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

      const getItemById = async (itemId: number) => {
        const fetchItems = async () => {
          try {
            const response = await fetch(`https://localhost:7092/api/InteractibleItems/api/interactibleItemByInteractibleId/${itemId}`);
            if (!response.ok) {
              throw new Error("Failed to fetch data");
            }
            const json = await response.json();
            return(json);
          } catch (error) {
            console.error(error);
          }
        };
        fetchItems();
      }

    
    
      return (
        <>
          <p>{lokace != null && lokace.name}</p>
          {/* {items &&
            items.map((item, index) => (
              <p style={{ cursor: "pointer", position: "absolute" }}
                onClick={() =>
                  setInventory((prevInventory: Item[]) => [
                    ...prevInventory,
                    item,
                  ])
                }
                key={index}
              >{`${item.name}`}</p>
            ))} */}
          {content &&
            content.map((content, index) => (
              <span
                style={{
                  cursor: "pointer",
                  position: "absolute",
                  bottom: `${content.yPos}%`,
                  left: `${content.xPos}%`,
                }}
              >
                <p key={index}>{content.interactibleID}</p>
                <ItemName itemId={content.interactibleID} />
                
              </span>
            ))}
        </>
      );
    };
    
    const ItemName: React.FC<{ itemId: number }> = ({ itemId }) => {
      const [itemName, setItemName] = useState<string | null>(null);

      useEffect(() => {
        const fetchItemName = async () => {
          const item = await getItemById(itemId);
          setItemName(item?.name || null);
        };
        fetchItemName();
      }, [itemId]);

      return <>{itemName}</>;
    };
    
    export default Content;