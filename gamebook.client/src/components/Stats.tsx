import React, { useContext, useEffect } from 'react';
import { GameContext } from '../context/GameContext';

import styles from '../styles/stats.module.css';
import { useNavigate } from 'react-router-dom';

const Stats: React.FC = () => {
  const navigate = useNavigate();
  const gameContext = useContext(GameContext);

  useEffect(() => {
    if (gameContext && (gameContext.radiation >= 100 || gameContext.hp <= 0)) {
      navigate("../Dialog/30");
    }
  }, [gameContext, navigate]);

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
      <p className={styles.energy}>⚡ {energy}</p>
      <p className={styles.radiation}>☢️ {radiation}</p>
      <p className={styles.money}>💰 {money}</p>
      {/* <button onClick={() => gameContext.setEnergy(energy - 10)}>-10⚡</button> */}
    </div>
  );
};

export default Stats;