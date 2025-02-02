import { InteractOption, Interactible, Item, InteractiblesItem } from "../types"
import { useContext, useEffect, useState } from "react"
import { GameContext } from "../context/GameContext"
import { domain } from "../utils"

interface OptionProps {
    interactOption: InteractOption;
    interactible: Interactible;
    key: string;
}

const Option: React.FC<OptionProps> = ({interactOption, interactible, key}) => {
      const [interactiblesItems, setInteractiblesItems] = useState<InteractiblesItem[]>([]);
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
    
          const [item, setItem] = useState<InteractiblesItem | undefined>(isItem(interactible.iteractibleID));

          
    
    const gameContext = useContext(GameContext);
          if (!gameContext) {
            throw new Error("GameContext is undefined");
          }
          const { inventory, setInventory, InteractiblesRemovedFromLocation, setInteractiblesRemovedFromLocation } = gameContext;
    
    return(
    <>
        {interactOption.optionID == 3 && item &&
        <p onClick={() => {
            setInventory((prevInventory: Item[]) => [...prevInventory, item.item]);
            setInteractiblesRemovedFromLocation((prevInteractiblesRemovedFromLocation: string[]) => [...prevInteractiblesRemovedFromLocation, key]);
            localStorage.setItem('inventory', JSON.stringify(inventory));
        }}
            </p>
}
    </>
    )
}

export default Option