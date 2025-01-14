import React, { useContext } from 'react';
import { GameContext } from '../context/GameContext';

import styles from '../styles/stats.module.css';

const Stats: React.FC = () => {
  const gameContext = useContext(GameContext);

  if (!gameContext) {
    return <div>Error: Game context is not available.</div>;
  }

  const { hp } = gameContext;
  const { energy } = gameContext;
    const { radiation } = gameContext;
    const { money } = gameContext;

  return (
    <div className={styles.stats}>
      <p className={styles.hp}>♥️ {hp}</p>
      <p className={styles.energy}>⚡️ {energy}</p>
      <p className={styles.radiation}>☢️ {radiation}</p>
      <p className={styles.money}>💰 {money}</p>
    </div>
  );
};

export default Stats;