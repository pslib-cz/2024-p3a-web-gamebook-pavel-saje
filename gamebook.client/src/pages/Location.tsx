import '../styles/Location.css';

import { Location } from '../types';

interface locationProps {
    lokace: Location | null;
}

const NetopyriVarle: React.FC<locationProps> = ({lokace}) =>{

    return(
        <>
            {lokace != null && <h2 className="title">{lokace.name}</h2>}
        </>
    )

}

export default NetopyriVarle