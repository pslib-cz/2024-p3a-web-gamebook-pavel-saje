import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { Item, DataLocation } from '../types';

interface GameContextProps {
  hp: number;
  setHp: React.Dispatch<React.SetStateAction<number>>;
  resetHp: () => void;
  energy: number;
  setEnergy: React.Dispatch<React.SetStateAction<number>>;
  resetEnergy: () => void;
  radiation: number;
  setRadiation: React.Dispatch<React.SetStateAction<number>>;
  resetRadiation: () => void;
  money: number;
  setMoney: React.Dispatch<React.SetStateAction<number>>;
  resetMoney: () => void;

  inventory: Item[];
  setInventory: React.Dispatch<React.SetStateAction<Item[]>>;
  resetInventory: () => void;

  defaultEnergy: number;

  canBeVisited: DataLocation[];
  setCanBeVisited: React.Dispatch<React.SetStateAction<DataLocation[]>>;
  resetCanBeVisited: () => void;

  //NOTE vzor: LocatioID-KEY
  InteractiblesRemovedFromLocation: string[];
  setInteractiblesRemovedFromLocation: React.Dispatch<React.SetStateAction<string[]>>;
  resetInteractiblesRemovedFromLocation: () => void;

  lastLocationId: number;
  setLastLocationId: React.Dispatch<React.SetStateAction<number>>;
  resetLastLocationId: () => void;

  resetAll: () => void;
}

export const GameContext = createContext<GameContextProps | undefined>(undefined);

export const GameProvider: React.FC<{ children: ReactNode }> = ({ children }) => {
  const defaultHp = 100;
  const [hp, setHp] = useState<number>(() => {
    const savedHp = localStorage.getItem('hp');
    return savedHp !== null ? parseInt(savedHp, 10) : defaultHp;
  });
  const defaultEnergy = 100;
  const [energy, setEnergy] = useState<number>(() => {
    const savedEnergy = localStorage.getItem("energy");
    return savedEnergy !== null ? parseInt(savedEnergy, 10) : defaultEnergy;
  });
  const defaultRadiation = 0;
const [radiation, setRadiation] = useState<number>(() => {
 const savedRadiation = localStorage.getItem("radiation");
      return savedRadiation !== null
        ? parseInt(savedRadiation, 10)
        : defaultRadiation;
    });
    const defaultMoney = 0;
    const [money, setMoney] = useState<number>(() => {
        const savedMoney = localStorage.getItem("money");
        return savedMoney !== null ? parseInt(savedMoney, 10) : defaultMoney;
    });

const [inventory, setInventory] = useState<Item[]>(() => {
    const savedInventory = localStorage.getItem("inventory");
    return savedInventory !== null ? JSON.parse(savedInventory) : [];
  });

  const [InteractiblesRemovedFromLocation, setInteractiblesRemovedFromLocation] = useState<string[]>(() => {
    const savedInteractiblesRemovedFromLocation = localStorage.getItem('InteractiblesRemovedFromLocation');
    return savedInteractiblesRemovedFromLocation !== null ? JSON.parse(savedInteractiblesRemovedFromLocation) : [];
  });

  const [canBeVisited, setCanBeVisited] = useState<DataLocation[]>(() => {
    const savedCanBeVisited = localStorage.getItem('canBeVisited');
    return savedCanBeVisited !== null ? JSON.parse(savedCanBeVisited) : [];
  });

  const [lastLocationId, setLastLocationId] = useState<number>(() => {
    const savedLastLocationId = localStorage.getItem('lastLocationId');
    return savedLastLocationId !== null ? parseInt(savedLastLocationId, 10) : 0;
  });



  useEffect(() => {
    if (hp !== undefined) localStorage.setItem('hp', hp.toString());
    if (energy !== undefined) localStorage.setItem('energy', energy.toString());
    if (radiation !== undefined) localStorage.setItem('radiation', radiation.toString());
    if (money !== undefined) localStorage.setItem('money', money.toString());
    if (inventory !== undefined) localStorage.setItem('inventory', JSON.stringify(inventory));
    if (InteractiblesRemovedFromLocation !== undefined) localStorage.setItem('InteractiblesRemovedFromLocation', JSON.stringify(InteractiblesRemovedFromLocation));
    if (canBeVisited !== undefined) localStorage.setItem('canBeVisited', JSON.stringify(canBeVisited));
    if (lastLocationId !== undefined) localStorage.setItem('lastLocationId', lastLocationId.toString());
  }, [hp, energy, radiation, money, inventory, InteractiblesRemovedFromLocation, canBeVisited, lastLocationId]);

  const resetHp = () => {
    setHp(defaultHp);
  };

  const resetEnergy = () => {
    setEnergy(defaultEnergy);
  }

    const resetRadiation = () => {
        setRadiation(defaultRadiation);
    }

    const resetMoney = () => {
        setMoney(defaultMoney);
    }

    const resetInventory = () => {
        setInventory([]);
    }

    const resetInteractiblesRemovedFromLocation = () => {
        setInteractiblesRemovedFromLocation([]);}

    const resetCanBeVisited = () => {
        setCanBeVisited([]);
    }

    const resetLastLocationId = () => {
        setLastLocationId(3);
    }

    const resetAll = () => {
        resetHp();
        resetEnergy();
        resetRadiation();
        resetMoney();
        resetInventory();
        resetInteractiblesRemovedFromLocation();
        resetCanBeVisited();
        resetLastLocationId();
    }

  return (
    <GameContext.Provider
      value={{
        hp,
        setHp,
        resetHp,
        energy,
        setEnergy,
        resetEnergy,
        radiation,
        setRadiation,
        resetRadiation,
        money,
        setMoney,
        resetMoney,
        inventory,
        setInventory,
        resetInventory,
        InteractiblesRemovedFromLocation,
        setInteractiblesRemovedFromLocation,
        resetInteractiblesRemovedFromLocation,
        defaultEnergy,
        canBeVisited,
        setCanBeVisited,
        resetCanBeVisited,
        resetAll,
        lastLocationId,
        setLastLocationId,
        resetLastLocationId
      }}
    >
      {children}
    </GameContext.Provider>
  );
};