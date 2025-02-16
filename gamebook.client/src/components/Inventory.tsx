import { useContext, useEffect, useState } from 'react';
import { GameContext } from '../context/GameContext';
import { FaTimes, FaBox } from 'react-icons/fa';
import { ConsumableItem, ItemTypes, Weapon, Item } from '../types';
import { domain } from '../utils';

import styles from '../styles/inventory.module.css';

const Inventory: React.FC = () => {
  const gameContext = useContext(GameContext);
  const [isVisible, setIsVisible] = useState(false);
  const [types, setTypes] = useState<ItemTypes[]>([]);

  const [consumables, setConsumables] = useState<ConsumableItem[]>([]);
  const [weapons, setWeapons] = useState<Weapon[]>([]);
  

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

  const {hp, defaultHp, setHp, equipedWeapon, setEquipedWeapon, energy, setEnergy, defaultEnergy, inventory, setInventory} = gameContext;

  useEffect(() => {
    const fetchTypes = async () => {
      const response = await fetch(`${domain}/api/Items/Consumables&Weapons`);
      const data = await response.json();

      setTypes(data);
      setWeapons(data.weapons);
      setConsumables(data.consumables);

      
    };
    fetchTypes();
  }, []);


  const findConsumable = (id: number) => {
    return consumables.find((consumable: ConsumableItem) => consumable.itemID === id);
  };

  const findWeapon = (id: number) => {
    return weapons.find((weapon: Weapon) => weapon.item.itemID === id);
  }


  const heal = (consumable: ConsumableItem) => {
    if (hp + consumable.healthValue < defaultHp) {
      setHp(hp + consumable.healthValue);
    } else {
      setHp(defaultHp);
    }
    if (energy + consumable.energyValue < defaultEnergy) {
      setEnergy(energy + consumable.energyValue);
    } else {
      setEnergy(defaultEnergy);
    }
  };

  const handleToggleInventory = () => {
    setIsVisible(!isVisible);
    if (!isVisible) {
      const updatedInventory = JSON.parse(localStorage.getItem('inventory') || '[]');
      setInventory(updatedInventory);
      const updatedEquipedWeapon = JSON.parse(localStorage.getItem('equipedWeapon') || '[]');
      setEquipedWeapon(updatedEquipedWeapon);
    }
  };

  return (
    <>
      <button onClick={handleToggleInventory}>
        {isVisible ? <FaTimes /> : <FaBox />}
      </button>
      {isVisible && (
        <div className={styles.inventory}>
          <h2>equiped weapon</h2>
          <p
            onClick={() => {
              
              setInventory((prevInventory: Item[]) => [
                ...prevInventory,
                ...(equipedWeapon ? [equipedWeapon.item] : []),
                
              ]);
              setEquipedWeapon(undefined);
              localStorage.setItem('equipedWeapon', JSON.stringify(undefined));
            }}
          >
            {equipedWeapon?.name}
          </p>
          <h2>Inventory</h2>
          <ul>
            {inventory.map((item, index) => (
              <li
                key={index}
                onClick={() => {
                  const updatedInventory = inventory.filter(
                    (invItem, invIndex) => invIndex !== index
                  );

                  const consumable = findConsumable(item.itemID);
                  if (consumable) {
                    heal(consumable);
                    setInventory(updatedInventory);
                  } else if (findWeapon(item.itemID) && !equipedWeapon) {
                    setInventory(updatedInventory);
                    setEquipedWeapon(findWeapon(item.itemID));
                  }    
                }}
              >
                <span>{index + 1}</span>
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