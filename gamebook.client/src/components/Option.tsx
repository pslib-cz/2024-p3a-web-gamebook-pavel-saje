import {
  InteractOption,
  Interactible,
  Item,
  InteractiblesItem,
} from "../types";
import { useContext, useEffect, useState } from "react";
import { GameContext } from "../context/GameContext";
import { domain } from "../utils";
import { Link } from "react-router-dom";
import styles from "../styles/options.module.css";

interface OptionProps {
  interactOption: InteractOption;
  interactible: Interactible;
  interactibleKey: string;
}

const Option: React.FC<OptionProps> = ({
  interactOption,
  interactible,
  interactibleKey,
}) => {
  // console.log("key", key)

  const [item, setItem] = useState<InteractiblesItem | undefined>();
  useEffect(() => {
    const fetchData = async () => {
      if (location) {
        try {
          const response = await fetch(`${domain}/api/DataInteractiblesItem`);
          const data = await response.json();
          setItem(
            data.find(
              (intItem: InteractiblesItem) =>
                intItem.interactibleID === interactible.interactibleID
            )
          );
        } catch (error) {}
      }
    };

    fetchData();
  }, [location]);

  // console.log(item, "is item ", isItem(interactible.iteractibleID))

  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error("GameContext is undefined");
  }
  const { inventory, setInventory, setInteractiblesRemovedFromLocation } =
    gameContext;

  return (
    <>
      {/* <p>{interactibleKey}</p> */}
      {(() => {
        switch (interactOption.optionID) {
          case 3:
            return (
              <p className={styles.option}
                onClick={() => {
                  if (item) {
                    setInventory((prevInventory: Item[]) => [
                      ...prevInventory,
                      item.item,
                    ]);
                    setInteractiblesRemovedFromLocation(
                      (prevInteractiblesRemovedFromLocation: string[]) => [
                        ...prevInteractiblesRemovedFromLocation,
                        interactibleKey,
                      ]
                    );
                    localStorage.setItem(
                      "inventory",
                      JSON.stringify(inventory)
                    );
                  }
                }}
              >
                {interactOption.optionText}
              </p>
            );
          case 2:
            return <p className={styles.option}>{interactOption.optionText}</p>;
          case 1:
            return (
              <Link className={styles.option}
                to={`/Fight/${interactible.interactibleID}&${interactibleKey}`}
              >
                {interactOption.optionText}
              </Link>
            );
          default:
            return null;
        }
      })()}
    </>
  );
};

export default Option;
