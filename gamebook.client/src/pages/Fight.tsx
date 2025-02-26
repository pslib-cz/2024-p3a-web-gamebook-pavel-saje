import { useParams } from "react-router-dom";
import Circle from "../components/Circle";
import "../styles/fight.css"
import { useEffect, useState } from "react";
import { domain } from "../utils";
import { NpcContent } from "../types/data";
import styles from '../styles/fight.module.css'

const Fight = () => {
  const {id} = useParams();

  const [data, setData] = useState<NpcContent>();

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${domain}/api/Npc/ByContentId/${id}`);
        if (!response.ok) {
          throw new Error("Failed to fetch data");
        }
        const json = await response.json();
        setData(json);
      } catch (error) {
        console.error(error);
      }
    };
    fetchData();
  }, [id]);

    return (
        <div className={styles.fight}>
          <h1>Fight</h1>
          <div>
            {data && <Circle npc={data.npc} content={data.content}/>}
          </div>
          <p>space pro potvrzení</p>
        </div>
    );
};

export default Fight;