import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import Map from '../components/map';
import NetopyriVarle from './Location';

import { Location } from '../types';
import { error } from 'console';

interface gameProps{
  energy: number;
}

const Game: React.FC<gameProps> = ({energy}) => {
    
    let { id } = useParams();
    const [currentLocation, setCurrentLocation] = useState<Location | null>(null);
    
  
    const [error, setError] = useState<Error | null>(null)
    const [loading, setLoading] = useState<boolean>(false)

    useEffect(() => {
        const fetchData = async () => {
          try{
            let response = await fetch(`https://localhost:7092/api/Locations/${id}?energy=${energy}`);
            if (!response.ok) {
              throw new Error("Failed to fetch data");
            }
            let json = await response.json();
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
        <Map />
        {loading && <p>Loading...</p>}
        {error && <p>Error: {error.message}</p>}
        {currentLocation && <NetopyriVarle lokace={currentLocation} />}
      </>
    );
}


export default Game;