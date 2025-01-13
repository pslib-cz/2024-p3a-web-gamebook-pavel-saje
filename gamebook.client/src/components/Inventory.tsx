import { useContext, useEffect, useState } from 'react';
import { GameContext } from '../context/GameContext';
import { FaTimes, FaBox } from 'react-icons/fa';
import { ConsumableItem } from '../types';

import styles from '../styles/inventory.module.css';

const Inventory: React.FC = () => {
  const gameContext = useContext(GameContext);
  const [inventory, setInventory] = useState(gameContext?.inventory || []);
  const [isVisible, setIsVisible] = useState(false);
  const [Consumables, setConsumables] = useState<ConsumableItem[]>([]);

  useEffect(() => {
    const handleStorageChange = () => {
      const updatedInventory = JSON.parse(localStorage.getItem('inventory') || '[]');
      setInventory(updatedInventory);
    };

    window.addEventListener('storage', handleStorageChange);

    return () => {
      window.removeEventListener('storage', handleStorageChange);
    };
  }, []);

  if (!gameContext) {
    return <div>Error: Game context is not available.</div>;
  }

  const { energy, setEnergy, defaultEnergy } = gameContext;

  useEffect(() => {
    const fetchConsumables = async () => {
      const response = await fetch('https://localhost:7092/api/Consumable');
      const data = await response.json();
      setConsumables(data);
    };
    fetchConsumables();
  }, []);
  const findConsumable = (id: number) => {
    return Consumables.find((consumable: ConsumableItem) => consumable.itemID === id);
  };
  const energyGivenByConsumable = (id: number) => {
    const consumable = findConsumable(id);
    if (consumable) {
      if (energy + consumable.energyValue <= defaultEnergy) {
        return consumable.energyValue;
      } else {
        return defaultEnergy;
      }
    }
    return 0;
  }

  const handleToggleInventory = () => {
    setIsVisible(!isVisible);
    if (!isVisible) {
      const updatedInventory = JSON.parse(localStorage.getItem('inventory') || '[]');
      setInventory(updatedInventory);
    }
  };

  return (
    <>
    {/* FIXME odebere všechny itemy stejného typu z inventu  */}
      <button onClick={handleToggleInventory}>
        {isVisible ? <FaTimes /> : <FaBox />}
      </button>
      {isVisible && (
        <div className={styles.inventory}>
          <h2>Inventory</h2>
          <ul>
            {inventory.map((item, index) => (
              <li
                key={index}
                onClick={() =>
                  findConsumable(item.itemID) &&
                  (
                  setEnergy(
                    (prevEn: number) =>
                      prevEn + energyGivenByConsumable(item.itemID)
                  ),
                  setInventory((prevInventory) =>
                    prevInventory.filter((i) => i.itemID !==
                      item.itemID)
                  )
                  )
                }
              >
                <span>{index+1}</span>
                <p>{item.name}</p>
              </li>
            ))}
          </ul>
        </div>
      )}
    </>
  );
};

export default Inventory;