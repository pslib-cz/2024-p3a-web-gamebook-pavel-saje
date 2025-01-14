
// import Map from '../components/map';
import MapWithGraph from '../components/map';
import NetopyriVarle from './Location';
import Stats from '../components/Stats';
import Inventory from '../components/Inventory';

import styles from '../styles/menu.module.css'


const Game: React.FC = () => {
  
    return (
      <>
        <Stats />
        <span className={styles.menu}><MapWithGraph /><Inventory/></span>
      
        <NetopyriVarle/>
      </>
    );
}


export default Game;