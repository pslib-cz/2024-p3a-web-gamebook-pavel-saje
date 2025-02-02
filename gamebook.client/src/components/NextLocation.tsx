import { useEffect, useState, useContext } from "react";
import {DataLocation} from "../types";

import { Link } from "react-router-dom";

import  styles from "../styles/nearBy.module.css";

import { GameContext } from "../context/GameContext";

interface NextLocationProps {
    locationId: number;
}

const NextLocation: React.FC<NextLocationProps> = ({locationId}) => {
    const [locations, setLocations] = useState<DataLocation[]>([]);
    const [error, setError] = useState<Error | null>(null)
    const [loading, setLoading] = useState<boolean>(false)

    const gameContext = useContext(GameContext);

    if (!gameContext) {
        throw new Error("GameContext is undefined");
    }

    useEffect(() => {
        const fetchData = async () => {
            try{
                const response = await fetch(`https://localhost:7092/api/Locations/${locationId}/connected`);
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                // console.log(json);
                setLocations(json);
                
            }
            catch (error) {
                if (error instanceof Error) {
                    setError(error)
                }
                else {
                    setError(new Error("neznámá chyba"))
                }
            }
            finally {
                setLoading(false)
            }
        }
        fetchData();
    })

    return (
      <>
        {loading && <p>Loading...</p>}
        {error && <p>{error.message}</p>}
        <main className={styles.main}>
          <h4 className={styles.h4}>Přejít do lokace</h4>
          <ul className={styles.ul}>
            {locations.map((location) => (
              <li key={location.locationID}>
                <Link
                  className={styles.link}
                  to={`/Game/${location.locationID}`}
                >
                  {location.name}
                </Link>
              </li>
            ))}
          </ul>
        </main>
      </>
    );
}

export default NextLocation;