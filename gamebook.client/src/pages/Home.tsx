import { Link } from "react-router-dom";

import { useContext } from 'react';

import { GameContext } from '../context/GameContext';

import styles from '../styles/home.module.css';

import LandscapeWarning from "../components/LandscapeWarning";
import { useOrientation } from "../hooks/useOrientation";

const Home = () => {
    const isLandscape = useOrientation();
    const gameContext = useContext(GameContext);
    if (!gameContext) {
        return <div>Error: Game context is not available.</div>;
    }

    const {resetAll, defaultLastLocation, lastLocation}  = gameContext;

    if (!isLandscape) {
        return <LandscapeWarning />;
    }

    return (
      <>
        <h1 className={styles.TITLE}>Stíny popela</h1>
        <div className={styles.menubar}>
          <Link
            className={styles.link}
            onClick={() => resetAll()}
            to={`Game/${defaultLastLocation.locationID}`}
          >
            Hrát si
          </Link>

          <Link className={styles.link} to={`Game/${lastLocation.locationID}`}>
            Pokračovat
          </Link>
        </div>
      </>
    );
};
export default Home;
