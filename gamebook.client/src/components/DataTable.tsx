import { useEffect, useState } from "react";

interface DataTableProps {
    endpoint: string;
}

const DataTable: React.FC<DataTableProps> = ({ endpoint }) => {
    const [data, setData] = useState<Array<object>>();
    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                const response = await fetch(endpoint);
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                setData(json);
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
    }, [endpoint]);

    useEffect(() => {
        console.log(data);
    }, [data]);

    return (
        <table className="table">
            <caption className="table-caption">
                <span>{endpoint.split("/").filter(Boolean).reverse()[0]}</span>
                <a className="button">+Add new</a>
            </caption>
            <thead>
                <tr className="table-row">
                    {data && data.length > 0 && (
                        <>
                            {Object.keys(data[0]).map((key) => (
                                <th key={key} className="table-cell">
                                    {key}
                                </th>
                            ))}
                            <th className="table-cell">CONFIG</th>
                        </>
                    )}
                </tr>
            </thead>
            <tbody>
                {data && data.length > 0 ? (
                    data.map((item, rowIndex) => (
                        <tr key={rowIndex} className="table-row">
                            {Object.keys(item).map((key) => (
                                <td
                                    key={`${rowIndex}-${key}`}
                                    className="table-cell"
                                >
                                    {typeof item[key] == "boolean" ? (
                                        <input
                                            type="checkbox"
                                            checked={item[key]}
                                            disabled={true}
                                        />
                                    ) : (
                                        item[key]
                                    )}
                                </td>
                            ))}
                            <td className="table-cell">
                                <a className="button">✏️Edit</a>
                                <a className="button button--danger">
                                    🗑️Delete
                                </a>
                            </td>
                        </tr>
                    ))
                ) : (
                    <tr>
                        <td className="table-cell">No data available</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};

export default DataTable;
