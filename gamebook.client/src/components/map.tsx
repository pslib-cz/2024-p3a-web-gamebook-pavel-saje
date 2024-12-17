import { useEffect, useState } from "react";
import { Link } from "react-router-dom";

import { Location } from "../types";

interface mapProps {
    curLoc: (curLoc: Location) => void;
}

const Map: React.FC<mapProps> = ({curLoc}) => {
  let [locations, setLocations] = useState<Location[] | null>(null);
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);


  useEffect(() => {
    const fetchData = async () => {
      setLoading(true);
      try {
        let response = await fetch("https://localhost:7092/api/Locations");
        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }
        let json = await response.json();
        console.log(json);
        setLocations(json);
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
  }, []);

  return (
    <>
      <ul className="map">
        {locations != null &&
          locations.map((lokace, index) => (
            <li key={index} className="card" onClick={() => curLoc(lokace)}>
              <Link to={`/Game/${lokace.locationID}`}>{lokace.name}</Link>
            </li>
          ))}
      </ul>
    </>
  );
};

export default Map;
