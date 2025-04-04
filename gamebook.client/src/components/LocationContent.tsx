import { useEffect, useState, useContext } from "react";
import { DataLocation, InteractiblesOption, LocationContent } from "../types";

import { GameContext } from "../context/GameContext";

import styles from "../styles/content.module.css";

import { domain } from "../utils";

import Option from "../components/Option";

import optionStyles from "../styles/options.module.css";
import Loading from "./Loading";

interface ContentProps {
    location: DataLocation | null;
}

const Content: React.FC<ContentProps> = ({ location }) => {
    const [interactiblesOptionList, setInteractiblesOptionList] = useState<
        InteractiblesOption[]
    >([]);
    const [loading, setLoading] = useState<boolean>(true);

    const gameContext = useContext(GameContext);
    if (!gameContext) {
        throw new Error("GameContext is undefined");
    }
    const { InteractiblesRemovedFromLocation } = gameContext;

    const [mousePosition, setMousePosition] = useState({ x: 0, y: 0 });
    const [showOptions, setShowOptions] = useState(false);
    const [showClick, setShowClick] = useState(false);
    const [hoveredContent, setHoveredContent] =
        useState<LocationContent | null>(null);
    const [hoveredOptions, setHoveredOptions] = useState<
        InteractiblesOption[] | null
    >(null);
    const [iKey, setIKey] = useState<string | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `${domain}/api/DataInteractiblesOption`
                );
                const data = await response.json();
                setInteractiblesOptionList(data);
                console.log("pryc", InteractiblesRemovedFromLocation);
            } catch (error) {
                console.error("Error fetching interactibles options", error);
            } finally {
                setLoading(false);
            }
        };
        fetchData();
        setShowClick(false);
        setShowOptions(false)
    }, [location, InteractiblesRemovedFromLocation]);

    

    useEffect(() => {
        const handleMouseMove = (event: MouseEvent) => {
            if (!showOptions) {
                setMousePosition({ x: event.clientX, y: event.clientY });
            }
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
        {loading && <Loading />}
        {/* TODO když je to npc a je mrtvé tak místo toho udělat náhrobek */}
        {location?.locationContents &&
          location.locationContents
            .map((content, index) => {
              // const options = hoveredOptions?.filter(
              //   (option) => option.interactibleID === content.interactibleID
              // );
              const key =
                location?.locationID +
                "-" +
                index +
                "-" +
                content.interactibleID;
              return !InteractiblesRemovedFromLocation.find((removed) => {
                return removed.locationContentID === content.locationContentID;
              }) ? (
                <>
                  <span
                    key={key}
                    className={styles.interactible}
                    style={{
                      position: "absolute",
                      transform: "translate(-50%, 50%)",
                      bottom: `${content.yPos}%`,
                      left: `${content.xPos}%`,
                      // backgroundColor: "red",
                      width: `${content.size}%`,
                      aspectRatio: 1,
                    }}
                    onMouseEnter={() => {
                      if (!hoveredOptions) {
                        setShowClick(true);
                      }
                      if (iKey != key) {
                        setHoveredContent(null);
                        setHoveredOptions(null);
                        setShowOptions(false);
                        setShowClick(true);
                      }
                      setHoveredContent(content);
                      setHoveredOptions(
                        getOptions(content.interactibleID) || []
                      );
                    }}
                    onMouseLeave={() => {
                      setShowClick(false);
                      if (!showOptions) {
                        setHoveredContent(null);
                        setHoveredOptions(null);
                      }
                    }}
                    onClick={() => {
                      setIKey(key);
                      setShowOptions(!showOptions);
                      setShowClick(!showClick);
                    }}
                  >
                    <img
                      style={{
                        width: `${100}%`,
                      }}
                      src={`${domain}/Uploads/${content.interactible.imagePath.replace(/\\/g, "/")}`}
                      alt={content.interactible.name}
                    />
                  </span>
                </>
              ) : null;
            })
            .filter(Boolean)}
        <ol
          className={optionStyles.optioncontainer}
          style={{
            top: mousePosition.y,
            left: mousePosition.x -16,
          }}
        >
          {showClick && !showOptions && (
            <>
              <ul>{hoveredContent?.interactible.name}</ul>
              <li>click to interact</li>
            </>
          )}
          {showOptions && (
            <ul onClick={() => setShowOptions(false)}>
              {hoveredContent &&
                hoveredOptions?.map((option) => (
                    <li>
                  <Option
                    content={hoveredContent}
                    interactOption={option.option}
                  />
                  </li>
                ))}
            </ul>
          )}
        </ol>
      </>
    );
};
export default Content;
