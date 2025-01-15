import React, { createContext, useState, useEffect, ReactNode } from 'react';
import { Item } from '../types';

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

  canBeVisited: Location[];
  setCanBeVisited: React.Dispatch<React.SetStateAction<Location[]>>;
  resetCanBeVisited: () => void;

  //NOTE vzor: LocatioID-KEY
  InteractiblesRemovedFromLocation: string[];
  setInteractiblesRemovedFromLocation: React.Dispatch<React.SetStateAction<string[]>>;
  resetInteractiblesRemovedFromLocation: () => void;


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

  const [canBeVisited, setCanBeVisited] = useState<Location[]>(() => {
    const savedCanBeVisited = localStorage.getItem('canBeVisited');
    return savedCanBeVisited !== null ? JSON.parse(savedCanBeVisited) : [];
  });



  useEffect(() => {
    localStorage.setItem('hp', hp.toString());
    localStorage.setItem('energy', energy.toString());
    localStorage.setItem('radiation', radiation.toString());
    localStorage.setItem('money', money.toString());

    localStorage.setItem('inventory', JSON.stringify(inventory));

    localStorage.setItem('InteractiblesRemovedFromLocation', JSON.stringify(InteractiblesRemovedFromLocation));

    localStorage.setItem('canBeVisited', JSON.stringify(canBeVisited));

  }, [hp, energy, radiation, money, inventory, InteractiblesRemovedFromLocation, canBeVisited]);

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

    const resetAll = () => {
        resetHp();
        resetEnergy();
        resetRadiation();
        resetMoney();
        resetInventory();
        resetInteractiblesRemovedFromLocation();
        resetCanBeVisited();
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
        resetAll
      }}
    >
      {children}
    </GameContext.Provider>
  );
};