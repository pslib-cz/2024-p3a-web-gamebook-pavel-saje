import "../styles/Location.css";
import Content from "../components/LocationContent";
import NextLocation from "../components/NextLocation";

import { useState, useEffect, useContext } from "react";
import { useParams } from "react-router-dom";

import { GameContext } from "../context/GameContext";

import { DataLocation, RequiredItems } from "../types";
import { domain } from "../utils";

const NetopyriVarle: React.FC = () => {
    const gameContext = useContext(GameContext);
    const inventory = gameContext ? gameContext.inventory : [];

    const { id } = useParams();
    const [currentLocation, setCurrentLocation] = useState<DataLocation | null>(
        null
    );
    const [targetLocation, setTargetLocation] = useState<DataLocation | null>(null);

    const [requiredItems, setRequiredItems] = useState<RequiredItems[]>([]);

    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `https://localhost:7092/api/DataLocation/${id}`
                );
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                console.log(json);
                setTargetLocation(json);
                console.log(targetLocation);
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

    console.log(targetLocation);

    return (
        <>
            {!loading && (
                <img style={{ width: "100%", height: "100vh" }}
                    src={
                        domain + targetLocation?.backgroundImagePath ||
                        "https://t4.ftcdn.net/jpg/00/89/02/67/360_F_89026793_eyw5a7WCQE0y1RHsizu41uhj7YStgvAA.jpg"
                    }
                    alt=""
                />
            )}

            <p>{error?.message}</p>
            {targetLocation != null && (
                <h2 className="title">{targetLocation.name}</h2>
            )}
            <Content lokace={targetLocation} />
            <NextLocation locationId={targetLocation?.locationID || 0} />
        </>
    );
};

export default NetopyriVarle;