import "../styles/Location.css";
import Content from "../components/LocationContent";
import NextLocation from "../components/NextLocation";

import { useState, useEffect, useContext } from "react";
import { useParams, useNavigate } from "react-router-dom";

import { GameContext } from "../context/GameContext";

import { Location, RequiredItems } from "../types";
import { domain } from "../utils";

const NetopyriVarle: React.FC = () => {
    const gameContext = useContext(GameContext);
    const energy = gameContext ? gameContext.energy : null;
    const inventory = gameContext ? gameContext.inventory : [];

    const { id } = useParams();
    const navigate = useNavigate();
    const [currentLocation, setCurrentLocation] = useState<Location | null>(
        null
    );
    const [targetLocation, setTargetLocation] = useState<Location | null>(null);

    const [requiredItems, setRequiredItems] = useState<RequiredItems[]>([]);

    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7092/api/Locations/${id}?energy=${energy}`
                );
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                console.log(json);
                setTargetLocation(json);
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

    useEffect(() => {
        const fetchRequiredItems = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7092/api/RequiredItems/GetByLocation/${targetLocation?.locationID}`
                );
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                console.log(json);
                setRequiredItems(json);

                // Check if all required items are in the inventory
                const requiredItemIds = json.map(
                    (item: RequiredItems) => item.itemID
                );
                const inventoryItemIds = inventory.map((item) => item.itemID);
                const allItemsPresent = requiredItemIds.every(
                    (itemId: number) => inventoryItemIds.includes(itemId)
                );

                if (allItemsPresent) {
                    setCurrentLocation(targetLocation);
                    if (targetLocation?.endID != null) {
                        navigate(`/Ending/${targetLocation.endID}`);
                    }
                } else {
                    setError(new Error("Nemáš všechny potřebné věci"));
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
        if (targetLocation) {
            fetchRequiredItems();
        }
    }, [targetLocation, inventory]);

    return (
        <>
            {!loading && (
                <img style={{ width: "100%", height: "100vh" }}
                    src={
                        domain + currentLocation?.backgroundImagePath ||
                        "https://t4.ftcdn.net/jpg/00/89/02/67/360_F_89026793_eyw5a7WCQE0y1RHsizu41uhj7YStgvAA.jpg"
                    }
                    alt=""
                />
            )}

            <p>{error?.message}</p>
            {currentLocation != null && (
                <h2 className="title">{currentLocation.name}</h2>
            )}
            <Content lokace={currentLocation} />
            <NextLocation locationId={currentLocation?.locationID || 0} />
        </>
    );
};

export default NetopyriVarle;