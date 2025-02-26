import { useEffect, useState, useContext } from "react";
import {
    DataLocation,
    Interactible,
    InteractiblesOption,
    LocationContent,
} from "../types";

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
    }, [location]);

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
        const handleMouseMove = (event: MouseEvent) => {
            !showOptions &&
                setMousePosition({ x: event.clientX, y: event.clientY });
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
                        return !InteractiblesRemovedFromLocation.find(
                            (removed) =>
                                removed.locationContentID ===
                                content.locationContentID
                            // return (1 > 0
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
                                        // backgroundColor: "red",
                                        width: `${content.size}%`,
                                        aspectRatio: 1,
                                    }}
                                    onMouseEnter={() => {
                                        !hoveredOptions && setShowClick(true);
                                        iKey != key &&
                                            (setHoveredContent(null),
                                            setHoveredOptions(null),
                                            setShowOptions(false),
                                            setShowClick(true));
                                        setHoveredContent(content);
                                        setHoveredOptions(
                                            getOptions(
                                                content.interactibleID
                                            ) || []
                                        );
                                    }}
                                    onMouseLeave={() => {
                                        setShowClick(false);
                                        !showOptions &&
                                            (setHoveredContent(null),
                                            setHoveredOptions(null));
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
                                            // height: "2rem"
                                        }}
                                        // src={`data:image/webp;base64,${content.interactible.imageBase64}`}
                                        // src="https://localhost:7092/Interactibles%5CFoodCan.png"
                                        src={`${domain}/${encodeURIComponent(
                                            content.interactible.imagePath
                                        )}`}
                                        alt={content.interactible.name}
                                    />
                                </span>
                            </>
                        ) : null;
                    })
                    .filter(Boolean)}
            <ul
                className={optionStyles.optioncontainer}
                style={{
                    position: "absolute",
                    top: mousePosition.y + 16,
                    left: mousePosition.x,
                }}
            >
                {showClick && !showOptions && (
                    <>
                        <li>{hoveredContent?.interactible.name}</li>
                        <li>click to interact</li>
                        <li>{hoveredContent?.locationContentID}</li>
                    </>
                )}
                {showOptions && (
                    <li onClick={() => setShowOptions(false)}>
                        {hoveredContent &&
                            hoveredOptions?.map((option) => (
                                <Option
                                    content={hoveredContent}
                                    interactOption={option.option}
                                />
                            ))}
                    </li>
                )}
            </ul>
        </>
    );
};
export default Content;
