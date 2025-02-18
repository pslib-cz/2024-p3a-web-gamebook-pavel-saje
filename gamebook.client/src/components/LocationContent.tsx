import { useEffect, useState, useContext } from "react";
import { DataLocation, Interactible, InteractiblesOption } from "../types";

import { GameContext } from "../context/GameContext";

import styles from "../styles/content.module.css";

import { domain } from "../utils";

import Option from "../components/Option";

import optionStyles from "../styles/options.module.css";

interface ContentProps {
  location: DataLocation | null;
}

const Content: React.FC<ContentProps> = ({ location }) => {
  const [interactiblesOptionList, setInteractiblesOptionList] = useState<
    InteractiblesOption[]
  >([]);

  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error("GameContext is undefined");
  }
  const { InteractiblesRemovedFromLocation } = gameContext;

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${domain}/api/DataInteractiblesOption`);
        const data = await response.json();
        setInteractiblesOptionList(data);
      } catch (error) {
        console.error("KOKOT");
      }
    };
    fetchData();
  }, [location]);

  const [mousePosition, setMousePosition] = useState({ x: 0, y: 0 });
  const [showOptions, setShowOptions] = useState(false);
  const [showClick, setShowClick] = useState(false);
  const [hoveredInteractible, setHoveredInteractible] =
    useState<Interactible | null>(null);
  const [hoveredOptions, setHoveredOptions] = useState<
    InteractiblesOption[] | null
  >(null);
  const [iKey, setIKey] = useState<string | null>(null);

  useEffect(() => {
    const handleMouseMove = (event: MouseEvent) => {
      !showOptions && setMousePosition({ x: event.clientX, y: event.clientY });
    };

    window.addEventListener("mousemove", handleMouseMove);

    return () => {
      window.removeEventListener("mousemove", handleMouseMove);
    };
  }, [showOptions]);

  const getOptions = (interactibleId: number): InteractiblesOption[] => {
    const list: InteractiblesOption[] = [];
    interactiblesOptionList.map(
      (option: InteractiblesOption) =>
        option.interactibleID === interactibleId && list.push(option)
    );
    return list;
  };

  return (
    <>
      {/* TODO když je to npc a je mrtvé tak místo toho udělat náhrobek */}
      {location?.locationContents &&
        location.locationContents
          .map((content, index) => {
            // const options = hoveredOptions?.filter(
            //   (option) => option.interactibleID === content.interactibleID
            // );
            const key = location?.locationID + "-" + index;
            return !InteractiblesRemovedFromLocation.find(
              (removed) => removed === key
            ) ? (
              <>
                <span
                  key={key}
                  className={styles.interactible}
                  style={{
                    position: "absolute",
                    transform: "translate(-50%, 50%)",
                    bottom: `${content.yPos}%`,
                    left: `${content.xPos}%`,
                  }}
                  onMouseEnter={() => {
                    !hoveredOptions && setShowClick(true);
                    iKey != key &&
                      (setHoveredInteractible(null),
                      setHoveredOptions(null),
                      setShowOptions(false),
                      setShowClick(true));
                    setHoveredInteractible(content.interactible);
                    setHoveredOptions(getOptions(content.interactibleID) || []);
                  }}
                  onMouseLeave={() => {
                    setShowClick(false);
                    !showOptions &&
                      (setHoveredInteractible(null), setHoveredOptions(null));
                  }}
                  onClick={() => {
                    setIKey(key);
                    setShowOptions(!showOptions);
                    setShowClick(!showClick);
                  }}
                >
                  {content.interactible.imageBase64 && (
                    <img
                      style={{ width: "7rem" }}
                      src={`data:image/webp;base64,${content.interactible.imageBase64}`}
                      alt={content.interactible.name}
                    />
                  )}
                </span>
              </>
            ) : null;
          })
          .filter(Boolean)}
      <ul
        className={optionStyles.option}
        style={{
          position: "absolute",
          top: mousePosition.y + 16,
          left: mousePosition.x,
        }}
      >
        {showClick && !showOptions && (
          <>
            <li>{hoveredInteractible?.name}</li>
            <li>click to interact</li>
          </>
        )}
        {showOptions && (
          <li onClick={() => setShowOptions(false)}>
            {hoveredInteractible &&
              hoveredOptions?.map((option) => (
                <Option
                  interactibleKey={iKey || ""}
                  interactOption={option.option}
                  interactible={hoveredInteractible}
                />
              ))}
          </li>
        )}
      </ul>
    </>
  );
};
export default Content;
