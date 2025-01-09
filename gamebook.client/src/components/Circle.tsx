import React, { useState, useEffect, useRef, useContext } from 'react';
import { GameContext } from '../context/GameContext';

const Circle: React.FC = () => {
  const duration = 2000;

  const emptyStart = 120; 
  const emptyEnd = emptyStart + 50;
  const filledEnd = emptyEnd + 10;

  const color = "#00FF00";

  const radius = 90;
  const circumference = 2 * Math.PI * radius;

  const emptyStartOffset = (emptyStart / 360) * circumference;
  const emptyEndOffset = (emptyEnd / 360) * circumference;
  const filledEndOffset = (filledEnd / 360) * circumference;

  const [currentAngle, setCurrentAngle] = useState(0);
  const rotationRef = useRef<SVGPolygonElement>(null);

  const [currentSegment, setCurrentSegment] = useState('circle');

  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error('GameContext must be used within a GameProvider');
  }
  const { hp, setHp } = gameContext;

  useEffect(() => {
    let angle = 0;
    const start = performance.now();
    const animate = (timestamp: number) => {
      const elapsed = (timestamp - start) % duration;
      angle = (360 * elapsed) / duration;
      setCurrentAngle(angle - 90);

      if (rotationRef.current) {
        rotationRef.current.setAttribute(
          'transform',
          `rotate(${angle}, 0, 0)`
        );
      }

      requestAnimationFrame(animate);
    };

    const animationFrame = requestAnimationFrame(animate);

    return () => cancelAnimationFrame(animationFrame);
  }, []);

  useEffect(() => {
    if (currentAngle >= emptyStart && currentAngle < emptyEnd) {
      setCurrentSegment('empty');
    } else if (currentAngle >= emptyEnd && currentAngle < filledEnd) {
      setCurrentSegment('filled');
    } else {
      setCurrentSegment('circle');
    }
  }, [currentAngle]);

  useEffect(() => {
    const handleKeyPress = (event: KeyboardEvent) => {
      if (event.code === 'Space') {
        console.log(currentSegment);
        if (currentSegment === 'circle') {
          console.log('hit');
          setHp((prevHp: number) => prevHp - 10);
        }
      }
    };

    window.addEventListener('keydown', handleKeyPress);

    return () => {
      window.removeEventListener('keydown', handleKeyPress);
    };
  }, [currentSegment, setHp]);

  return (
    <div>
      <svg viewBox="0 0 230 230">
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

      <p>Current Angle: {currentAngle.toFixed(0)}°</p>
      <p>{currentSegment}</p>
      <p>HP: {hp}</p>
    </div>
  );
};

export default Circle;
