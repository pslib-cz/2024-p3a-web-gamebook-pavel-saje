import '../styles/Location.css';
import Content from '../components/LocationContent';

import { useState, useEffect, useContext } from 'react';
import { useParams } from 'react-router-dom';

import { GameContext } from '../context/GameContext';

import { Location, RequiredItems } from '../types';

const NetopyriVarle: React.FC = () =>{

  const gameContext = useContext(GameContext);
  const energy = gameContext ? gameContext.energy : null;

    const { id } = useParams();
    const [currentLocation, setCurrentLocation] = useState<Location | null>(null);
    const [targetLocation, setTargetLocation] = useState<Location | null>(null);
    
    const [requiredItems, setRequiredItems] = useState<RequiredItems[]>([]);
  
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

    useEffect(() => {
      const fetchRequiredItems = async () => {
        try{
          const response = await fetch(`https://localhost:7092/api/RequiredItems/GetByLocation/${currentLocation?.locationID}`);
          if (!response.ok) {
            throw new Error("Failed to fetch data");
          }
          const json = await response.json();
          console.log(json);
          setRequiredItems(json);
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
      fetchRequiredItems();
    }, [currentLocation]);


    return (
      <>
        {currentLocation != null && <h2 className="title">{currentLocation.name}</h2>}
        <Content lokace={currentLocation} />
      </>
    );

}

export default NetopyriVarle