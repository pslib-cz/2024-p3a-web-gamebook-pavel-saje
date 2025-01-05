import React, { createContext, useState, useEffect, ReactNode } from 'react';

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

  useEffect(() => {
    localStorage.setItem('hp', hp.toString());
    localStorage.setItem('energy', energy.toString());
    localStorage.setItem('radiation', radiation.toString());
  }, [hp, energy, radiation, money]);

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
        resetMoney
      }}
    >
      {children}
    </GameContext.Provider>
  );
};