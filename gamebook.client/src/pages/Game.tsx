
// import Map from '../components/map';
import MapWithGraph from '../components/map';
import NetopyriVarle from './Location';
import Stats from '../components/Stats';
import Inventory from '../components/Inventory';



const Game: React.FC = () => {    
    return (
      <>
        <Stats />
        <span className='map'><MapWithGraph /><Inventory/></span>
      
        <NetopyriVarle/>
      </>
    );
}


export default Game;