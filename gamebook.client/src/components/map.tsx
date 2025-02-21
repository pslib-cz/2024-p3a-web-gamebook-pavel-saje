import React, { useEffect, useState, useRef, useContext } from "react";
import ForceGraph2D from "react-force-graph-2d";
import { useNavigate } from "react-router-dom";
import { FaMap, FaTimes } from "react-icons/fa";
import styles from "../styles/menu.module.css";
import { GameContext } from "../context/GameContext";

const MapWithGraph2D: React.FC = () => {
  const [data, setData] = useState<any[] | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [showGraph, setShowGraph] = useState<boolean>(false);
  const navigate = useNavigate();
  const graphRef = useRef<any>(null);

  const gameContext = useContext(GameContext);

  if (!gameContext) {
    throw new Error("GameContext is undefined");
  }

  const { discoveredLocations } = gameContext;

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const response = await fetch(
          "https://localhost:7092/api/Locations/connections"
        );
        const jsonData = await response.json();
        console.log("Data (raw):", jsonData);
        setData(jsonData);
      } catch (error) {
        if (error instanceof Error) {
          setError(error);
        } else {
          setError(new Error("Unknown error"));
        }
      } finally {
        setLoading(false);
      }
    };

    fetchData();
  }, []);

  const calculateNodePosition = (data: any[]) => {
    const spacing = 200;
    const locationMap = new Map();

    // Iterate over data to ensure unique locations
    data.forEach((item) => {
      if (discoveredLocations.includes(item.firstNode.locationID)) {
        if (!locationMap.has(item.firstNode.locationID)) {
          locationMap.set(item.firstNode.locationID, {
            id: item.firstNode.locationID.toString(),
            name: item.firstNode.name,
            x: (locationMap.size % 5) * spacing,
            y: Math.floor(locationMap.size / 5) * spacing,
          });
        }
      }

      if (discoveredLocations.includes(item.secondNode.locationID)) {
        if (!locationMap.has(item.secondNode.locationID)) {
          locationMap.set(item.secondNode.locationID, {
            id: item.secondNode.locationID.toString(),
            name: item.secondNode.name,
            x: (locationMap.size % 5) * spacing,
            y: Math.floor(locationMap.size / 5) * spacing,
          });
        }
      }
    });

    console.log("Unique locations:", Array.from(locationMap.values()));
    return Array.from(locationMap.values());
  };

  const createEdges = (data: any[]) => {
    // Create edges between locations
    return data
      .filter(
        (path) =>
          discoveredLocations.includes(path.firstNodeID) &&
          discoveredLocations.includes(path.secondNodeID)
      )
      .map((path) => ({
        source: path.firstNodeID.toString(),
        target: path.secondNodeID.toString(),
        label: `Cost: ${path.energyTravelCost}`,
      }));
  };

  const handleNodeClick = (node: any) => {
    setShowGraph(false);
    navigate(`/Game/${node.id}`);
  };

  return (
    <div>
      {!showGraph ? (
        <button
          className="map--button"
          onClick={() => setShowGraph(true)}
          title="Open Map"
        >
          <FaMap />
        </button>
      ) : (
        <button onClick={() => setShowGraph(false)} title="Close Map">
          <FaTimes />
        </button>
      )}

      {showGraph && (
        <div className={styles.map}>
          {loading && <p>Loading...</p>}
          {error && <p>Error: {error.message}</p>}
          {!loading && !error && data && (
            <ForceGraph2D
              ref={graphRef}
              graphData={{
                nodes: calculateNodePosition(data),
                links: createEdges(data),
              }}
              nodeAutoColorBy="group"
              linkWidth={2}
              onNodeClick={handleNodeClick}
              nodeCanvasObject={(node, ctx, globalScale) => {
                const label = `${node.id} ${node.name}`;
                const fontSize = 12 / globalScale;
                ctx.font = `${fontSize}px Sans-Serif`;
                ctx.fillStyle = "black";
                ctx.textAlign = "center";
                ctx.textBaseline = "middle";
                ctx.fillText(label, node.x as number, (node.y as number) - 10);

                const radius = 8;
                ctx.beginPath();
                ctx.arc(
                  node.x as number,
                  node.y as number,
                  radius,
                  0,
                  2 * Math.PI,
                  false
                );
                ctx.fillStyle = node.color || "brown";
                ctx.fill();
              }}
              width={window.innerWidth}
              height={window.innerHeight}
              maxZoom={10}
              minZoom={1}
              enableNodeDrag={false}
            />
          )}
        </div>
      )}
    </div>
  );
};

export default MapWithGraph2D;
