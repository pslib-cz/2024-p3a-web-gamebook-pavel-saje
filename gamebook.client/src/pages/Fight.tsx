import { useParams } from "react-router-dom";
import Circle from "../components/Circle";
import "../styles/fight.css"
import { useEffect, useState } from "react";
import { domain } from "../utils";
import { Npc } from "../types/data";

const Fight = () => {
  const {id} = useParams();

  const parts = id?.split("&") ?? [];

  const iKEy = parts[1] ?? null;
  const npcID = parts[0] ? parseInt(parts[0]) : null;

  const [npc, setNpc] = useState<Npc>();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${domain}/api/Npc/ByInteractibleId/${npcID}`);
        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }
        const json = await response.json();
        setNpc(json);
      } catch (error) {
        console.error(error);
      }
    };
    fetchData();
  }, [id]);

    return (
        <div className="fight">
          <h1>Fight</h1>
          <div className="circle">
            {npc && <Circle npc={npc} iKey={iKEy ?? ''}/>}
          </div>
          <p>space pro potvrzení</p>
        </div>
    );
};

export default Fight;