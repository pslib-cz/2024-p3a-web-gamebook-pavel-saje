import '../styles/Location.css';
import Content from '../components/LocationContent';
import NextLocation from '../components/NextLocation';

import { Location } from '../types';

interface locationProps {
    lokace: Location | null;
}

const NetopyriVarle: React.FC<locationProps> = ({lokace}) =>{

    return (
      <>
<<<<<<< HEAD
      <p>{error?.message}</p>
        {currentLocation != null && <h2 className="title">{currentLocation.name}</h2>}
        <Content lokace={currentLocation} />
        <NextLocation locationId={currentLocation?.locationID || 0} />
=======
        {lokace != null && <h2 className="title">{lokace.name}</h2>}
        <Content lokace={lokace} />
>>>>>>> ad75db67f9e1adcfbaa63202a19c44dbe9439afc
      </>
    );

}

export default NetopyriVarle