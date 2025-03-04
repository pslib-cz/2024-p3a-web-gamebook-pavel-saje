import {
  InteractOption,
  Item,
  InteractiblesItem,
  LocationContent
} from "../types";
import { useContext, useEffect, useState } from "react";
import { GameContext } from "../context/GameContext";
import { domain } from "../utils";
import { Link } from "react-router-dom";
import styles from "../styles/options.module.css";

interface OptionProps {
  interactOption: InteractOption;
  content: LocationContent;
}

const Option: React.FC<OptionProps> = ({
  interactOption,
  content,
}) => {
  // console.log("key", key)

  const [item, setItem] = useState<InteractiblesItem | undefined>();
  useEffect(() => {
    const fetchData = async () => {
      if (location) {
        try {
          const response = await fetch(`${domain}/api/InteractiblesItem`);
          const data = await response.json();
          setItem(
            data.find(
              (intItem: InteractiblesItem) =>
                intItem.interactibleID === content.interactible.interactibleID
            )
          );
        } catch (error) {
          console.log(error)
        }
      }
    };

    fetchData();
  }, [location]);

  // console.log(item, "is item ", isItem(interactible.iteractibleID))

  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error("GameContext is undefined");
  }
  const {
    setInventory,
    setInteractiblesRemovedFromLocation,
  } = gameContext;

  return (
    <>
      {/* <p>{interactibleKey}</p> */}
      {(() => {
        switch (interactOption.optionID) {
          case 4:
            return(
              <Link className={styles.option} to={`/Trade/${content.interactible.interactibleID}`}>{interactOption.optionText}</Link>
            )
          case 3:
            return (
              <p className={styles.option}
                onClick={() => {
                  if (item) {
                    setInventory((prevInventory: Item[]) => {
                      const newInventory = [...prevInventory, item.item];
                      localStorage.setItem("inventory", JSON.stringify(newInventory));
                      return newInventory;
                    });
                    setInteractiblesRemovedFromLocation(
                      (prevInteractiblesRemoved: LocationContent[]) => {
                        const newInteractiblesRemoved = [...prevInteractiblesRemoved, content];
                        localStorage.setItem("InteractiblesRemovedFromLocation", JSON.stringify(newInteractiblesRemoved));
                        return newInteractiblesRemoved;
                      }
                    );
                  }
                }}
              >
                {interactOption.optionText}
              </p>
            );
          case 2:
            return <Link to={`/Dialog/${content.interactible.interactibleID}&true`} className={styles.option}>{interactOption.optionText}</Link>;
          case 1:
            return (
              <Link className={styles.option}
                to={`/Fight/${content.locationContentID}`}
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
