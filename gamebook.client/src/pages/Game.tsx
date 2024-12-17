import { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';

import Map from '../components/map';
import NetopyriVarle from './Location';

import { Location } from '../types';

const Game: React.FC = () => {
    
    let { id } = useParams();
    const [currentLocation, setCurrentLocation] = useState<Location | null>(null);
    console.log(id)

    useEffect(() => {
        const fetchData = async () => {
            let response = await fetch(`https://localhost:7092/api/Locations/${id}`);
            if (!response.ok) {
              throw new Error("Failed to fetch data");
            }
            let json = await response.json();
            console.log(json);
            setCurrentLocation(json);
        };
        fetchData();
      }, []);

    return (
      <>
        <Map curLoc={setCurrentLocation}/>
        <NetopyriVarle lokace={currentLocation}/>
      </>
    );
}


export default Game;