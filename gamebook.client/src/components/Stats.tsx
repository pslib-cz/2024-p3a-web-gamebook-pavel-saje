import React, { useContext } from 'react';
import { GameContext } from '../context/GameContext';

import styles from '../styles/stats.module.css';

const Stats: React.FC = () => {
  const gameContext = useContext(GameContext);

  if (!gameContext) {
    return <div>Error: Game context is not available.</div>;
  }

  const { hp, money, energy, setEnergy, radiation } = gameContext;

  return (
    <div className={styles.stats}>
      <p className={styles.hp}>♥️ {hp}</p>
      <p className={styles.energy}>⚡️ {energy}<button onClick={() => setEnergy((prevEn: number) => prevEn - 10)}>-10</button></p>
      <p className={styles.radiation}>☢️ {radiation}</p>
      <p className={styles.money}>💰 {money}</p>
    </div>
  );
};

export default Stats;