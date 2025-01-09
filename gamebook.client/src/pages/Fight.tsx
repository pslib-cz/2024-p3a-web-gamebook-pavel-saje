import Circle from "../components/Circle";
import "../styles/fight.css"

const Fight = () => {
    return (
        <div className="fight">
          <h1>Fight</h1>
          <div className="circle">
            <Circle />
          </div>
          <p>space pro potvrzení</p>
        </div>
    );
};

export default Fight;