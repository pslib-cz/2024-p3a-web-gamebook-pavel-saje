import { useEffect, useState } from "react";
import { Location, LocationContent } from "../types";

interface ContentProps {
    lokace: Location | null;
    }

const Content: React.FC<ContentProps> = ({lokace}) => {
    const [contents , setContents] = useState<LocationContent[] | null>(null);

  useEffect(() => {
    const fetchData = async () => {
      try{
        const response = await fetch(`https://localhost:7092/api/InteractibleItems/GetInteractibleIDs/${lokace?.locationID}`);
        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }
        const json = await response.json();
        console.log(`contentjson : ${json}`);
        setContents(json);
        console.log(`content : ${contents}`);
      }
      catch (error) {
        console.error(error);
      }
    };
    fetchData();

  }, [lokace]);
  
    return (
      <>
        <p>{lokace != null && lokace.name}</p>
        {contents && contents.map((content, index) => (
          <p key={index}>{`ID : ${content}`}</p>
        ))}
      </>
    );
}

export default Content;