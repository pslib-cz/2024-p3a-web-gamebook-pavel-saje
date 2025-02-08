import { Link, useParams } from 'react-router-dom';
import styles from '../styles/end.module.css';
import { useEffect, useState } from 'react';
import {End}  from '../types';
import { domain } from '../utils';

const Ending = () => {
    const { id } = useParams();

    const [end, setEnd] = useState<End | null>(null);

    useEffect(() => {
        const fetchEnd = async () => {
            try {
                const response = await fetch(`${domain}/api/End/${id}`);
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
                {/* <h2>{end.name}</h2> */}
                <p>{end.text}</p>
                <Link to="/">Menu</Link>
            </div>
            }
        </>
    );
};

export default Ending;