import { Link, useParams } from 'react-router-dom';
import styles from '../styles/end.module.css';
import { useEffect, useState } from 'react';
import {End}  from '../types';

const Ending = () => {
    const { id } = useParams();

    const [end, setEnd] = useState<End | null>(null);

    useEffect(() => {
        const fetchEnd = async () => {
            try {
                const response = await fetch(`https://localhost:7092/api/End/${id}`);
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                console.log(json);
                setEnd(json);
            } catch (error) {
                console.error(error);
            }
        }
        fetchEnd();
    },[id])

    return (
        <>
            {end && 
            <div className={styles.end}>
                <h2>{end.name}</h2>
                <p>{end.description}</p>
                <Link to="/">Menu</Link>
            </div>
            }
        </>
    );
};

export default Ending;