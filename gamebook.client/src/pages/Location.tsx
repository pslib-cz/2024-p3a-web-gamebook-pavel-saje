import "../styles/Location.css";
import Content from "../components/LocationContent";
import NextLocation from "../components/NextLocation";

import { useState, useEffect, useContext } from "react";
import { useParams, useNavigate } from "react-router-dom";

import { GameContext } from "../context/GameContext";

import { DataLocation, RequiredItems } from "../types";
import { domain } from "../utils";

const NetopyriVarle: React.FC = () => {
    const gameContext = useContext(GameContext);

    const navigate = useNavigate();

    const { id } = useParams();
    const [targetLocation, setTargetLocation] = useState<DataLocation | null>(
        null
    );

    if (!gameContext) {
        return <div>Error: Game context is not available.</div>;
    }

    const {
        lastLocation,
        setLastLocation,
        radiation,
        setRadiation,
        inventory,
    } = gameContext;

    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7092/api/Locations/${id}`
                );
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                const requiredItemIds =
                    json?.requiredItems.map(
                        (item: RequiredItems) => item.itemID
                    ) || [];

                const inventoryItemIds = inventory.map((item) => item.itemID);
                const allItemsPresent = requiredItemIds.every(
                    (itemId: number) => inventoryItemIds.includes(itemId)
                );

                console.log("requireditemsids ", requiredItemIds);

                if (allItemsPresent) {
                    setTargetLocation(json);
                    setLastLocation(json);
                    localStorage.setItem("lastLocationId", json.toString());
                } else {
                    navigate(`/Game/${lastLocation}`);
                    alert("nemáš potřebné věci pro lokaci");
                }
            } catch (error) {
                if (error instanceof Error) {
                    setError(error);
                } else {
                    setError(new Error("neznámá chyba"));
                }
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, [id]);

    console.log(targetLocation);

    if (targetLocation) {
        console.log("inventory", inventory);
        const totalRadiationGain =
            targetLocation.radiationGain +
            inventory.reduce((acc, item) => acc + item.radiationGain, 0);
        setRadiation(totalRadiationGain);
    }

    return (
        <>
            {/* {!loading && (
                <img style={{ width: "100%", height: "100vh" }}
                    src={
                        domain + targetLocation?.backgroundImagePath ||
                        "https://t4.ftcdn.net/jpg/00/89/02/67/360_F_89026793_eyw5a7WCQE0y1RHsizu41uhj7YStgvAA.jpg"
                    }
                    alt=""
                />
            )} */}

            {targetLocation && targetLocation.backgroundImageBase64 && (
                <img
                    style={{
                        width: "100%",
                        height: "100vh",
                        userSelect: "none",
                        pointerEvents: "none",
                    }}
                    src={`data:image/webp;base64,${targetLocation.backgroundImageBase64}`}
                    alt={targetLocation.name}
                />
            )}

            <p>{error?.message}</p>
            {targetLocation != null && (
                <h2 className="title">{targetLocation.name}</h2>
            )}
            <Content location={targetLocation} />
            <NextLocation locationId={targetLocation?.locationID || 0} />
        </>
    );
};

export default NetopyriVarle;
