import React, {  useEffect, useRef, useContext, useReducer } from 'react';
import { GameContext } from '../context/GameContext';
import { LocationContent, Npc } from '../types/data';
import { useNavigate } from 'react-router-dom';
import { domain } from '../utils';
import styles from '../styles/fight.module.css'

type FightState = {
  currentAngle: number;
  wholeRotation: number;
  npcsHp: number;
  isResetting: boolean;
};

type FightAction =
  | { type: 'RESET_ROTATION' }
  | { type: 'UPDATE_ANGLE'; payload: number }
  | { type: 'HANDLE_HIT'; payload: { 
      emptyStart: number;
      emptyEnd: number;
      filledEnd: number;
      weaponDamage: number;
      npcWeaponDamage: number;
      setHp: (cb: (prevHp: number) => number) => void;
    } 
  };

const fightReducer = (state: FightState, action: FightAction): FightState => {
  switch (action.type) {
    case 'RESET_ROTATION':
      return {
        ...state,
        wholeRotation: Math.random() * 360,
        currentAngle: 0,
        isResetting: false
      };
    case 'UPDATE_ANGLE':
      return {
        ...state,
        currentAngle: action.payload - 90
      };
    case 'HANDLE_HIT':
      { const { emptyStart, emptyEnd, filledEnd, weaponDamage, npcWeaponDamage, setHp } = action.payload;
      if (state.currentAngle >= emptyStart && state.currentAngle < emptyEnd) {
        return {
          ...state,
          npcsHp: state.npcsHp - (weaponDamage || 1),
          isResetting: true
        };
      } else if (state.currentAngle >= emptyEnd && state.currentAngle < filledEnd) {
        return {
          ...state,
          npcsHp: state.npcsHp - (weaponDamage || 1) * 2,
          isResetting: true
        };
      } else {
        setHp((prevHp: number) => prevHp - npcWeaponDamage);
        return {
          ...state,
          isResetting: true
        };
      } }
    default:
      return state;
  }
};

interface CircleProps {
  npc: Npc;
  content: LocationContent;
}

const Circle: React.FC<CircleProps> = ({npc, content}) => {
  
  const emptyStart = 120; 
  const emptyEnd = emptyStart + 30;
  const filledEnd = emptyEnd + 10;
  
  const color = "#00FF00";
  
  const radius = 90;
  const circumference = 2 * Math.PI * radius;
  
  const emptyStartOffset = (emptyStart / 360) * circumference;
  const emptyEndOffset = (emptyEnd / 360) * circumference;
  const filledEndOffset = (filledEnd / 360) * circumference;
  
  const initialState: FightState = {
    currentAngle: 0,
    wholeRotation: 0,
    npcsHp: npc.health,
    isResetting: false
  };

  const [state, dispatch] = useReducer(fightReducer, initialState);
  const rotationRef = useRef<SVGPolygonElement>(null);
  
  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error('GameContext must be used within a GameProvider');
  }
  const { hp, setHp, equipedWeapon, lastLocation, setInteractiblesRemovedFromLocation } = gameContext;
  const navigate = useNavigate();
  
  const fast = 600;
  const slow = 1700;
  const duration = equipedWeapon ? (100 - equipedWeapon.damage)*14 >= fast ? (100 - equipedWeapon.damage)*14 : fast : slow;

  useEffect(() => {
    let animationFrame: number;
    const start = performance.now();
  
    const animate = (timestamp: number) => {
      if (state.isResetting) {
        dispatch({ type: 'RESET_ROTATION' });
        if (rotationRef.current) {
          rotationRef.current.setAttribute("transform", "rotate(0, 0, 0)");
        }
        return;
      }
  
      const elapsed = (timestamp - start) % duration;
      const angle = (360 * elapsed) / duration;
      dispatch({ type: 'UPDATE_ANGLE', payload: angle });
  
      if (rotationRef.current) {
        rotationRef.current.setAttribute("transform", `rotate(${angle}, 0, 0)`);
      }
  
      animationFrame = requestAnimationFrame(animate);
    };
  
    animationFrame = requestAnimationFrame(animate);
  
    return () => cancelAnimationFrame(animationFrame);
  }, [duration, state.isResetting]);

  useEffect(() => {
    const handleKeyPress = (event: KeyboardEvent) => {
      if (event.code === "Space") {
        dispatch({ 
          type: 'HANDLE_HIT',
          payload: {
            emptyStart,
            emptyEnd,
            filledEnd,
            weaponDamage: equipedWeapon?.damage ?? 1,
            npcWeaponDamage: npc.weapon.damage,
            setHp
          }
        });
      }
    };
  
    window.addEventListener("keydown", handleKeyPress);
    return () => {
      window.removeEventListener("keydown", handleKeyPress);
    };
  }, [state.currentAngle, emptyEnd, equipedWeapon?.damage, filledEnd, npc.weapon.damage, setHp]);

  useEffect(() => {
    if (hp <= 0) {
      navigate("../Dialog/30")
    }
    if (state.npcsHp <= 0) {
      alert("ty vrahu")
      navigate(`/game/${lastLocation.locationID}`);
      setInteractiblesRemovedFromLocation((prev: LocationContent[]) => [...prev, content]);
    }
  }, [content, hp, lastLocation.locationID, navigate, state.npcsHp, setInteractiblesRemovedFromLocation]);

  return (
    <div className={styles.circle}>
      <div className={styles.npc}>
        <img
style={{
  WebkitFilter: `drop-shadow(4px 4px 2px rgba(255, 0, 0, ${1 - (state.npcsHp/npc.health)})) drop-shadow(-4px -4px 2px rgba(255, 0, 0, ${1 - (state.npcsHp/npc.health)}))`,
  filter: `drop-shadow(4px 4px 2px rgba(255, 0, 0, ${1 - (state.npcsHp/npc.health)})) drop-shadow(-4px -4px 2px rgba(255, 0, 0, ${1 - (state.npcsHp/npc.health)}))`
}}
          src={`${domain}/${encodeURIComponent(
            content.interactible.imagePath
          )}`}
          alt={content.interactible.name}
        />
        <ul>
          <li>{npc.weapon.name}</li>
          <li>{npc.weapon.damage}</li>
          <li>{state.npcsHp}</li>
        </ul>
      </div>
      <svg
        viewBox="0 0 230 230"
        style={{ transform: `rotate(${state.wholeRotation}deg)` }}
      >
        <circle
          cx="110"
          cy="110"
          r={radius}
          stroke={color}
          strokeWidth="2"
          fill="none"
        />
        <circle
          cx="110"
          cy="110"
          r={radius}
          stroke={color}
          strokeWidth="20"
          strokeDasharray={`${
            emptyEndOffset - emptyStartOffset
          } ${circumference}`}
          strokeDashoffset={-emptyStartOffset}
          fill="none"
          strokeOpacity="0.5"
        />
        <circle
          cx="110"
          cy="110"
          r={radius}
          stroke={color}
          strokeWidth="20"
          strokeDasharray={`${
            filledEndOffset - emptyEndOffset
          } ${circumference}`}
          strokeDashoffset={-emptyEndOffset}
          fill="none"
        />
        <g transform="translate(110, 110)">
          <polygon
            ref={rotationRef}
            points="0,-100 10,-110 -10,-110"
            fill={color}
            stroke="black"
          />
        </g>
      </svg>

      <ul>
        <li>{equipedWeapon?.name}</li>
        <li>{equipedWeapon?.damage}</li>
      </ul>
    </div>
  );
};

export default Circle;
