import { useParams } from "react-router-dom";
import Circle from "../components/Circle";
import "../styles/fight.css";
import { useContext, useEffect, useState } from "react";
import { domain } from "../utils";
import { NpcContent } from "../types/data";
import styles from "../styles/fight.module.css";
import { GameContext } from "../context/GameContext";

const Fight = () => {
    const { id } = useParams();
    const gameContext = useContext(GameContext);
    if (!gameContext) {
        throw new Error("GameContext is undefined");
    }
    const [data, setData] = useState<NpcContent>();

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(
                    `${domain}/api/Npc/ByContentId/${id}`
                );
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
                {data && <Circle npc={data.npc} content={data.content} />}
            </div>
            <div className={styles.attackhelper}>
                <p className={styles.spacekey}>SPACE</p>
                <p className={styles.text}>
                    {`Tvoje zbraň: ${
                        gameContext.equipedWeapon?.name || "Holé ruce"
                    } ${gameContext.equipedWeapon?.damage || 1}⚔️`}
                </p>
            </div>
        </div>
    );
};

export default Fight;
