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
                            linkWidth={2}
                            onNodeClick={handleNodeClick}
                            nodeCanvasObject={(node, ctx, globalScale) => {
                                // Set node color individually
                                if (
                                    gameContext.lastLocation.locationID ==
                                    node.id
                                ) {
                                    node.color = "black";
                                } else {
                                    node.color = "#88ff00";
                                }

                                const label = node.name;
                                const fontSize = 12 / globalScale;
                                ctx.font = `${fontSize}px Sans-Serif`;
                                ctx.fillStyle = "black";
                                ctx.textAlign = "center";
                                ctx.textBaseline = "middle";
                                ctx.fillText(label, node.x, node.y - 10);

                                const radius = 8;
                                ctx.beginPath();
                                ctx.arc(
                                    node.x,
                                    node.y,
                                    radius,
                                    0,
                                    2 * Math.PI,
                                    false
                                );
                                ctx.fillStyle = node.color;
                                ctx.shadowBlur = 8;
                                ctx.shadowColor = "black";
                                ctx.stroke(
                                    new Path2D(
                                        "m 55.591072,67.56647 0.252113,17.143733 m 10,0 -0.504227,-17.395843 1.134513,9.832432 7.56341,-0.126055 0.252114,-9.20215 0.126058,16.135276 7.563411,-16.891617 3.529592,16.765561 -1.386626,-6.681015 -6.428899,-0.504227 m 15,8 -0.252114,-16.009221 -6.302843,-0.126058 22.312066,-0.252113 -10.46272,0.252113 -0.126058,16.261334 11.092998,0.378172 -11.345112,-0.630285 0.504228,-7.689469 5.924674,0.126058 m 25,0 0.630282,-13.109915 5.924672,11.723285 0.756343,-12.35357 m 5,0 0.252114,15.37893 m 10,-10 -6.302843,-1.260571 -2.39508,4.538046 1.638739,9.45426 5.294386,-0.12605 0.630286,-5.294388 -3.529592,-0.126058 m 15,-8 -6.302843,-1.260571 -2.39508,4.538046 1.638739,9.45426 5.294386,-0.12605 0.630286,-5.294388 -3.529592,-0.126058 m 10,0 0.630282,12.983855 7.815527,-0.37817 -7.689469,-0.37817 -0.126058,-5.042276 6.176788,-0.504227 h -6.302843 z m 0,0 h 6.428898 m 10,10 0.630282,-12.479629 6.428898,1.764797 -5.798613,3.907761 4.285933,6.554961 m 10,0 -3.52959,-2.269024 -4.41199,3.529592 7.68947,4.159877 -3.40354,5.042277 -3.7817,-2.39508"
                                    )
                                );
                                ctx.fill();
                            }}
                            width={window.innerWidth * 0.8}
                            height={window.innerHeight * 0.8}
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
