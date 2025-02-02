import { Link } from "react-router-dom";

import { useContext } from 'react';

import { GameContext } from '../context/GameContext';

import styles from '../styles/home.module.css';

const Home = () => {
    const gameContext = useContext(GameContext);
    if (!gameContext) {
        return <div>Error: Game context is not available.</div>;
    }

    const {resetAll, defaultLastLocationId, lastLocationId}  = gameContext;
    return (
        <>
            <h1 className={styles.TITLE}>Stíny popela</h1>
            <div className={styles.menubar}>
                <Link className={styles.link} onClick={() => resetAll()} to={`Game/${defaultLastLocationId}`}>Play</Link>

                <Link className={styles.link} to={`Game/${lastLocationId}`}>Continue</Link>

                <Link className={styles.link} to="/Admin">Admin</Link>
                </div>


            <p className={styles.p} onClick={() => alert("YOU ARE KOKOT🐒🐒🐒AND NIGGA🐒🐒🐒")}>
                Jsem kokot
            </p>
        </>
    );
};
export default Home;
