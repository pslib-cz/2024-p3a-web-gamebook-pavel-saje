import { useEffect, useState, useContext } from "react";
import { Location, LocationContent, Item } from "../types";

import { GameContext } from '../context/GameContext';

interface ContentProps {
    lokace: Location | null;
    }

const Content: React.FC<ContentProps> = ({lokace}) => {
  const [items, setItems] = useState<Item[] | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try{
        const response = await fetch(`https://localhost:7092/api/InteractibleItems/GetInteractibleIDs/${lokace?.locationID}`);
        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }
        const json = await response.json();
        setItems(json);
      }
      catch (error) {
        console.error(error);
      }
    };
    fetchData();

  }, [lokace]);
  
    return (
      <>
        <p>{lokace != null && lokace.name}</p>
        {items && items.map((item, index) => (
          <p key={index}>{`${item.name}`}</p>
        ))}
      </>
    );
}

export default Content;