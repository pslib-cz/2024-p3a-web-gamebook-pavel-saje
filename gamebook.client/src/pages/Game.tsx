import MapWithGraph from '../components/map';
import NetopyriVarle from './Location';
import Stats from '../components/Stats';
import Inventory from '../components/Inventory';
import styles from '../styles/menu.module.css';
import LandscapeWarning from "../components/LandscapeWarning";
import { useOrientation } from "../hooks/useOrientation";

const Game: React.FC = () => {
    const isLandscape = useOrientation();

    if (!isLandscape) {
        return <LandscapeWarning />;
    }
  
    return (
        <>
            <Stats />
            <span className={styles.menu}>
                <MapWithGraph />
                <Inventory/>
            </span>
            <NetopyriVarle/>
        </>
    );
}

export default Game;