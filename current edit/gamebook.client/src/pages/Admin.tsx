import React, { useContext } from "react";
import { GameContext } from "../context/GameContext";

const Admin = () => {
  const gameContext = useContext(GameContext);

  if (!gameContext) {
    return <div>Error: Game context is not available.</div>;
  }

  const { hp, resetHp } = gameContext;
  const { energy, resetEnergy } = gameContext;
    const { radiation, resetRadiation } = gameContext;
    const { money, resetMoney } = gameContext;

  return (
    <>
      <h1>SEXADMIN</h1>

      <div>
        <p>{hp}</p>
        <button onClick={resetHp}>Reset HP</button>
      </div>
      <div>
        <p>{energy}</p>
        <button onClick={resetEnergy}>Reset Energy</button>
      </div>
      <div>
        <p>{radiation}</p>
        <button onClick={resetRadiation}>Reset Radiation</button>
      </div>
      <div>
        <p>{money}</p>
        <button onClick={resetMoney}>Reset Money</button>
      </div>
    </>
  );
};
export default Admin;
