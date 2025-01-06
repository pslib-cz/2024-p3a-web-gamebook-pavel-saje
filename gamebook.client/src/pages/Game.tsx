import { useState, useEffect, useContext } from 'react';
import { useParams } from 'react-router-dom';

import { GameContext } from '../context/GameContext';

// import Map from '../components/map';
import MapWithGraph from '../components/map';
import NetopyriVarle from './Location';
import Stats from '../components/Stats';

import { Location } from '../types';


const Game: React.FC = () => {

    const gameContext = useContext(GameContext);
    const energy = gameContext ? gameContext.energy : null;

    const { id } = useParams();
    const [currentLocation, setCurrentLocation] = useState<Location | null>(null);
    
  
    const [error, setError] = useState<Error | null>(null)
    const [loading, setLoading] = useState<boolean>(false)

    useEffect(() => {
        const fetchData = async () => {
          try{
            const response = await fetch(`https://localhost:7092/api/Locations/${id}?energy=${energy}`);
            if (!response.ok) {
              throw new Error("Failed to fetch data");
            }
            const json = await response.json();
            console.log(json);
            setCurrentLocation(json);
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
        };
        fetchData();
      }, [id]);
      console.log(currentLocation)
    return (
      <>
        <Stats />
        <MapWithGraph />
        {loading && <p>Loading...</p>}
        {error && <p>Error: {error.message}</p>}
        {currentLocation && <NetopyriVarle lokace={currentLocation} />}
      </>
    );
}


export default Game;