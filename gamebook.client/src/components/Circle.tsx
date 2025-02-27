import React, { useState, useEffect, useRef, useContext } from 'react';
import { GameContext } from '../context/GameContext';
import { LocationContent, Npc } from '../types/data';
import { useNavigate } from 'react-router-dom';
import { domain } from '../utils';
import styles from '../styles/fight.module.css'


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
  
  const [currentAngle, setCurrentAngle] = useState(0);
  const rotationRef = useRef<SVGPolygonElement>(null);
  
  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error('GameContext must be used within a GameProvider');
  }
  const { hp, setHp, equipedWeapon, lastLocation, setInteractiblesRemovedFromLocation } = gameContext;
  const [isResetting, setIsResetting] = useState(false);
  const navigate = useNavigate();
  
  const fast = 600;
  const slow = 1700;
  const duration = equipedWeapon ? (100 - equipedWeapon.damage)*14 >= fast ? (100 - equipedWeapon.damage)*14 : fast : slow;

  const [wholeRotation, setWholeRotation] = useState(0);

  const [npcsHp, setNpcsHp] = useState(npc.health);

  useEffect(() => {
    let angle = 0;
    let animationFrame: number;
    const start = performance.now();
  
    const animate = (timestamp: number) => {
      if (isResetting) {
        setWholeRotation(Math.random() * 360);
        setCurrentAngle(0);
        if (rotationRef.current) {
          rotationRef.current.setAttribute("transform", "rotate(0, 0, 0)");
        }
        setIsResetting(false);
        return;
      }
  
      const elapsed = (timestamp - start) % duration;
      angle = (360 * elapsed) / duration;
      setCurrentAngle(angle - 90);
  
      if (rotationRef.current) {
        rotationRef.current.setAttribute("transform", `rotate(${angle}, 0, 0)`);
      }
  
      animationFrame = requestAnimationFrame(animate);
    };
  
    animationFrame = requestAnimationFrame(animate);
  
    return () => cancelAnimationFrame(animationFrame);
  }, [isResetting]);

  useEffect(() => {
    const handleKeyPress = (event: KeyboardEvent) => {
      if (event.code === "Space") {
        setIsResetting(true);
        
        if (currentAngle >= emptyStart && currentAngle < emptyEnd) { //empty
          setNpcsHp((prevNpcsHp: number) => prevNpcsHp - (equipedWeapon?.damage || 1));
        }
        else if (currentAngle >= emptyEnd && currentAngle < filledEnd) { //filled
          console.log("hit");
          setNpcsHp((prevNpcsHp: number) => prevNpcsHp - (equipedWeapon?.damage || 1) * 2);
        }
        else{
          console.log("hit");
          setHp((prevHp: number) => prevHp - npc.weapon.damage);
        }
      }
    };
  
    window.addEventListener("keydown", handleKeyPress);
    return () => {
      window.removeEventListener("keydown", handleKeyPress);
    };
  }, [currentAngle]);

  useEffect(() => {
    if (hp <= 0) {
      navigate("../Dialog/30")
    }
    if (npcsHp <= 0) {
      alert("ty vrahu")
      navigate(`/game/${lastLocation.locationID}`);
      setInteractiblesRemovedFromLocation((prevInteractiblesRemovedFromLocation: LocationContent[]) => {
        return [...prevInteractiblesRemovedFromLocation, content];
      });
    }
  }, [hp, npcsHp]);

  return (
    <div className={styles.circle}>
      <div className={styles.npc}>
        <img
style={{
  WebkitFilter: `drop-shadow(4px 4px 2px rgba(255, 0, 0, ${1 - (npcsHp/npc.health)})) drop-shadow(-4px -4px 2px rgba(255, 0, 0, ${1 - (npcsHp/npc.health)}))`,
  filter: `drop-shadow(4px 4px 2px rgba(255, 0, 0, ${1 - (npcsHp/npc.health)})) drop-shadow(-4px -4px 2px rgba(255, 0, 0, ${1 - (npcsHp/npc.health)}))`
}}
          src={`${domain}/${encodeURIComponent(
            content.interactible.imagePath
          )}`}
          alt={content.interactible.name}
        />
        <ul>
          <li>{npc.weapon.name}</li>
          <li>{npc.weapon.damage}</li>
          <li>{npcsHp}</li>
        </ul>
      </div>
      <svg
        viewBox="0 0 230 230"
        style={{ transform: `rotate(${wholeRotation}deg)` }}
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
