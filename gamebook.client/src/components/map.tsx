/*
import React, { useEffect, useState } from "react";
import ReactFlow, { Node, Edge } from "react-flow-renderer";
import { useNavigate } from "react-router-dom";
import { Location } from "../types";

const MapWithGraph: React.FC = () => {
  const [locations, setLocations] = useState<Location[] | null>(null);
  const [paths, setPaths] = useState<any[] | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  const navigate = useNavigate();

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const locationsResponse = await fetch("https://localhost:7092/api/Locations");
        const pathsResponse = await fetch("https://localhost:7092/api/LocationPaths");
        const locationsJson = await locationsResponse.json();
        const pathsJson = await pathsResponse.json();
        setLocations(locationsJson);
        setPaths(pathsJson);
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

  const calculateNodePosition = (index: number, totalNodes: number, radius: number = 200) => {
    const angle = (2 * Math.PI * index) / totalNodes;
    const x = radius * Math.cos(angle);
    const y = radius * Math.sin(angle);
    return { x, y };
  };

  const nodes: Node[] = locations?.map((location, index) => {
    const position = calculateNodePosition(index, locations.length);
    return {
      id: location.locationID.toString(),
      data: { label: location.name },
      position,
      onClick: () => {
        navigate(`/Game/${location.locationID}`);
      },
    };
  }) || [];

  const edges: Edge[] = paths
    ?.filter((path) => {
      const firstNodeExists = locations?.some((loc) => Number(loc.locationID) === Number(path.firstNodeID));
      const secondNodeExists = locations?.some((loc) => Number(loc.locationID) === Number(path.secondNodeID));
      return firstNodeExists && secondNodeExists;
    })
    .map((path) => ({
      id: `edge-${path.pathID}`,
      source: path.firstNodeID.toString(),
      target: path.secondNodeID.toString(),
      label: `Cost: ${path.energyTravelCost}`,
    })) || [];

  return (
    <div style={{ height: "100vh", width: "100%" }}>
      {loading && <p>Loading...</p>}
      {error && <p>Error: {error.message}</p>}
      {!loading && !error && (
        <ReactFlow nodes={nodes} edges={edges} fitView attributionPosition="top-right" />
      )}
    </div>
  );
};

export default MapWithGraph;
*/

//NOTE 3D
import React, { useEffect, useState, useRef } from "react";
import ForceGraph3D from "3d-force-graph";
import { useNavigate } from "react-router-dom";
import { Location } from "../types";

const MapWithGraph: React.FC = () => {
    const [locations, setLocations] = useState<Location[] | null>(null);
    const [paths, setPaths] = useState<any[] | null>(null);
    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);
    const navigate = useNavigate();
    const graphContainerRef = useRef<HTMLDivElement | null>(null);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                const locationsResponse = await fetch(
                    "https://localhost:7092/api/Locations"
                );
                const pathsResponse = await fetch(
                    "https://localhost:7092/api/LocationPaths"
                );
                const locationsJson = await locationsResponse.json();
                const pathsJson = await pathsResponse.json();
                console.log("Locations (raw):", locationsJson);
                console.log("Paths (raw):", pathsJson);
                setLocations(locationsJson);
                setPaths(pathsJson);
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

    // Function to create node positions (force-directed layout will handle most of it)
    const calculateNodePosition = (locations: Location[]) => {
        return locations.map((location, index) => ({
            id: location.locationID.toString(),
            name: location.name,
            x: Math.random() * 1000,
            y: Math.random() * 1000,
            z: Math.random() * 1000,
        }));
    };

    // Create edges based on paths
    const createEdges = (locations: Location[], paths: any[]) => {
        return (
            paths
                ?.filter((path) => {
                    const firstNodeExists = locations?.some(
                        (loc) =>
                            Number(loc.locationID) === Number(path.firstNodeID)
                    );
                    const secondNodeExists = locations?.some(
                        (loc) =>
                            Number(loc.locationID) === Number(path.secondNodeID)
                    );
                    return firstNodeExists && secondNodeExists;
                })
                .map((path) => ({
                    source: path.firstNodeID.toString(),
                    target: path.secondNodeID.toString(),
                    label: `Cost: ${path.energyTravelCost}`,
                })) || []
        );
    };

    const handleNodeClick = (node: any) => {
        navigate(`/Game/${node.id}`);
    };

    useEffect(() => {
        if (graphContainerRef.current && locations && paths) {
            const graph = new ForceGraph3D(graphContainerRef.current)
                .graphData({
                    nodes: calculateNodePosition(locations),
                    links: createEdges(locations, paths),
                })
                .nodeAutoColorBy("group")
                .linkWidth(2)
                .linkColor(() => "#cccccc")
                .linkDirectionalParticles(4)
                .linkDirectionalParticleSpeed(0.01)
                .onNodeClick(handleNodeClick);
        }
    }, [locations, paths]);

    return (
        <div style={{ height: "100vh", width: "100%" }}>
            {loading && <p>Loading...</p>}
            {error && <p>Error: {error.message}</p>}
            {!loading && !error && (
                <div
                    ref={graphContainerRef}
                    style={{ width: "100%", height: "100%" }}
                />
            )}
        </div>
    );
};

export default MapWithGraph;

/*
import React, { useEffect, useState, useRef } from "react";
import ForceGraph2D from "react-force-graph-2d";
import { useNavigate } from "react-router-dom";
import { Location } from "../types";
import { FaMap, FaTimes } from "react-icons/fa";

const MapWithGraph2D: React.FC = () => {
  const [locations, setLocations] = useState<Location[] | null>(null);
  const [paths, setPaths] = useState<any[] | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);
  const [showGraph, setShowGraph] = useState<boolean>(false);
  const navigate = useNavigate();
  const graphRef = useRef<any>(null);

  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        const locationsResponse = await fetch("https://localhost:7092/api/Locations");
        const pathsResponse = await fetch("https://localhost:7092/api/LocationPaths");
        const locationsJson = await locationsResponse.json();
        const pathsJson = await pathsResponse.json();
        console.log("Locations (raw):", locationsJson);
        console.log("Paths (raw):", pathsJson);
        setLocations(locationsJson);
        setPaths(pathsJson);
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

  const calculateNodePosition = (locations: Location[]) => {
    const spacing = 200;
    return locations.map((location, index) => ({
      id: location.locationID.toString(),
      name: location.name,
      x: (index % 5) * spacing,
      y: Math.floor(index / 5) * spacing,
    }));
  };

  const createEdges = (locations: Location[], paths: any[]) => {
    return paths
      ?.filter((path) => {
        const firstNodeExists = locations?.some(
          (loc) => Number(loc.locationID) === Number(path.firstNodeID)
        );
        const secondNodeExists = locations?.some(
          (loc) => Number(loc.locationID) === Number(path.secondNodeID)
        );
        return firstNodeExists && secondNodeExists;
      })
      .map((path) => ({
        source: path.firstNodeID.toString(),
        target: path.secondNodeID.toString(),
        label: `Cost: ${path.energyTravelCost}`,
      })) || [];
  };

  const handleNodeClick = (node: any) => {
    setShowGraph(false);
    navigate(`/Game/${node.id}`);
  };

  return (
    <div>
      {!showGraph ? (
        <button className="map--button"
          onClick={() => setShowGraph(true)}
          title="Open Map"
        >
          <FaMap />
        </button>
      ) : (
        <button
          onClick={() => setShowGraph(false)}
          title="Close Map"
        >
          <FaTimes />
        </button>
      )}

      {showGraph && (
        <div className="map--graph">
          {loading && <p>Loading...</p>}
          {error && <p>Error: {error.message}</p>}
          {!loading && !error && locations && paths && (
            <ForceGraph2D
              ref={graphRef}
              graphData={{
                nodes: calculateNodePosition(locations),
                links: createEdges(locations, paths),
              }}
              nodeAutoColorBy="group"
              linkWidth={2}
              linkDirectionalParticles={2}
              linkDirectionalParticleSpeed={0.01}
              onNodeClick={handleNodeClick}
              nodeCanvasObject={(node, ctx, globalScale) => {
                const label = node.name;
                const fontSize = 12 / globalScale;
                ctx.font = `${fontSize}px Sans-Serif`;
                ctx.fillStyle = "black";
                ctx.textAlign = "center";
                ctx.textBaseline = "middle";
                ctx.fillText(label, node.x as number, (node.y as number) - 10);

                const radius = 8;
                ctx.beginPath();
                ctx.arc(node.x as number, node.y as number, radius, 0, 2 * Math.PI, false);
                ctx.fillStyle = node.color || "blue";
                ctx.fill();
              }}
              width={window.innerWidth}
              height={window.innerHeight}
            />
          )}
        </div>
      )}
    </div>
  );
};

export default MapWithGraph2D;
\*/
