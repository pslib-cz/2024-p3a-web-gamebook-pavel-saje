import React, { useContext } from 'react';
import { GameContext } from '../context/GameContext';

const Inventory: React.FC = () => {
  const gameContext = useContext(GameContext);

    if (!gameContext) {
        return <div>Error: Game context is not available.</div>;
    }

    const { inventory } = gameContext;

  return (
    <>
        <h2>Inventory</h2>
        <ul>
            {inventory.map((item, index) => (
            <li key={index}>{item.name}</li>
            ))}
        </ul>
    </>
  );
};

export default Inventory;