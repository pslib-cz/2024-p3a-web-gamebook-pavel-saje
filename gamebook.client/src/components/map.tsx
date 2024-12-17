import { useEffect, useState } from "react";

import { Location } from "../types";

const Map = () => {
    let [locations, setLocations] = useState<Location[] | null>(null);
    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            setLoading(true);
            try {
                let response = await fetch('https://localhost:7092/api/Locations');
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                let json = await response.json();
                console.log(json);
                setLocations(json);
            } catch (error) {
                if (error instanceof Error) {
                    setError(error);
                } else {
                    setError(new Error("neznámá chyba"));
                }
            } finally {
                setLoading(false);
            }
        };
        fetchData();
    }, []);

    return (
        <>
            <p>ses curak</p>
            {locations != null &&
                locations.map((lokace, index) => (
                    <div key={index} className="card">
                        <p>{lokace.name}</p>
                    </div>
                ))}
        </>
    )
}

export default Map;