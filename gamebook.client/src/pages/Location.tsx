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
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(
          `${domain}/api/Locations/${id}${
            lastLocation && `?currentId=${lastLocation.locationID}`
          }`
        );
        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }
        const json = await response.json();
        const requiredItemIds =
          json?.requiredItems.map((item: RequiredItems) => item.itemID) || [];
          
          const inventoryItemIds = inventory.map((item) => item.itemID);
          const allItemsPresent = requiredItemIds.every((itemId: number) =>
          inventoryItemIds.includes(itemId)
        );
        
        if (!allItemsPresent) {
          navigate(`/Game/${lastLocation.locationID}`);
          alert("némáš potřebné věci pro lokaci");
        } else if (json?.travelCost > energy) {
          navigate(`/Game/${lastLocation.locationID}`);

          alert("nemáš dostatek energie na cestu");
        } else {
          setTargetLocation(json);
          setLastLocation(json);
          
          if (json?.travelCost) {
            setEnergy((prevEnergy: number) => prevEnergy - json.travelCost);
          }
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
          console.log(error)
        } else {
          setError(new Error("neznámá chyba"));
        }
      } finally {
        setLoading(false);
      }
    };
    fetchData();
  }, [id]);
  
  if (!gameContext) {
    return <div>Error: Game context is not available.</div>;
  }

  const {
    lastLocation,
    setLastLocation,
    setRadiation,
    inventory,
    setEnergy,
    energy,
  } = gameContext;
  if (targetLocation) {
    const totalRadiationGain =
      targetLocation.radiationGain +
      inventory.reduce((acc, item) => acc + item.radiationGain, 0);
    setRadiation(totalRadiationGain);
  }
  if (error) {
    return <div>Error: {error.message}</div>;
  }

  if (targetLocation?.end != null) {
    navigate(`/Dialog/${targetLocation.end[0].DialogID}`);
  }
  return (
    <>
      <img
        style={{
          width: "100%",
          height: "100vh",
          userSelect: "none",
          pointerEvents: "none",
        }}
        src={
          targetLocation
            ? `${domain}/${encodeURIComponent(
                targetLocation.backgroundImagePath
              )}`
            : ""
        }
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
