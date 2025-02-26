import "../styles/Location.css";
import Content from "../components/LocationContent";
import NextLocation from "../components/NextLocation";

import { useState, useEffect, useContext } from "react";
import { useParams, useNavigate } from "react-router-dom";

import { GameContext } from "../context/GameContext";

import { DataLocation, RequiredItems } from "../types";
import { domain } from "../utils";
import Loading from "../components/Loading";

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
        setEnergy,
        energy
    } = gameContext;

    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    // https://localhost:7092/api/ShortestPath?FirstLocationId=3&SecondLocationId=3

    // ?currentId=4

    useEffect(() => {
        const fetchData = async () => {
            try {
                
                const response = await fetch(
                    `https://localhost:7092/api/Locations/${id}${lastLocation && `?currentId=${lastLocation.locationID}`}`
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

                // console.log("requireditemsids ", requiredItemIds);
                console.log("1", "target:", targetLocation, "last:", lastLocation, "json", json)
                if (!allItemsPresent) {
                    console.log("lastlocation = ", lastLocation)
                    navigate(`/Game/${lastLocation.locationID}`);
                    alert("némáš potřebné věci pro lokaci")
                } else if(json?.travelCost > energy){
                    console.log("lastlocation = ", lastLocation)
                    navigate(`/Game/${lastLocation.locationID}`);
                    
                    alert("nemáš dostatek energie na cestu")
                }
                 else {
                    setTargetLocation(json);
                    setLastLocation(json);
                    // console.log(json)
                    // console.log("targetLocation = ", targetLocation)
                    
                    if(json?.travelCost){
                        setEnergy((prevEnergy: number) => prevEnergy - json.travelCost)
                    }

                    // localStorage.setItem("lastLocationId", json.toString());
                    console.log("2", "target:", targetLocation, "last:", lastLocation, "json", json)
                }
                if (targetLocation) {
                    const totalRadiationGain =
                        targetLocation.radiationGain +
                        inventory.reduce((acc, item) => acc + item.radiationGain, 0);
                    setRadiation(totalRadiationGain);
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


    

    if (targetLocation) {
        // console.log("inventory", inventory);
        const totalRadiationGain =
            targetLocation.radiationGain +
            inventory.reduce((acc, item) => acc + item.radiationGain, 0);
        setRadiation(totalRadiationGain);
    }

    if(targetLocation?.end != null){ 
        // console.log(targetLocation?.end[0].endID);
        navigate("/Dialog/1")
    } 
    return (
        <>
        {/* {loading && <p>Načítám...</p>} */}
        {/* {alert(targetLocation?.end.LocatioID)} */}
                <img
                    style={{
                        width: "100%",
                        height: "100vh",
                        userSelect: "none",
                        pointerEvents: "none",
                    }}
                    src={targetLocation ? `${domain}/${encodeURIComponent(targetLocation.backgroundImagePath)}` : ""}
                    // src={`data:image/webp;base64,${targetLocation.backgroundImageBase64}`}
                    alt={targetLocation?.name}
                />

            {loading && <Loading />}
            {targetLocation != null && (
                <h2 className="title">{targetLocation.name}</h2>
            )}
            <Content location={targetLocation} />
            <NextLocation locationId={targetLocation?.locationID || 0} />
        </>
    );
};

export default NetopyriVarle;
