import { useEffect, useState } from "react";
import { EndpointHeader } from "../types/admin";
import { domain } from "../utils";

type DataTableProps = {
    endpoint: string;
    header: EndpointHeader;
};

const DataTable: React.FC<DataTableProps> = ({ endpoint, header }) => {
    type DataItem = Record<string, string | number | boolean>;
    const [data, setData] = useState<Array<DataItem>>();
    const [error, setError] = useState<Error | null>(null);
    const [loading, setLoading] = useState<boolean>(false);

    useEffect(() => {
        const fetchData = async () => {
            try {
                setLoading(true);
                const response = await fetch(endpoint);
                if (!response.ok) {
                    throw new Error("Failed to fetch data");
                }
                const json = await response.json();
                setData(json);
                setLoading(false);
            } catch (error) {
                setData([]);
                if (error instanceof Error) {
                    setError(error);
                } else {
                    setError(new Error("Unknown chyba"));
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

    const deleteData = async (id: number) => {
        try {
            console.log(`${endpoint}/${id}`);
            const response = await fetch(`${endpoint}/${id}`, {
                method: "DELETE",
            });
            if (!response.ok) {
                throw new Error("Delete failed");
            }
            setData((prevData) =>
                (prevData ?? []).filter((item: DataItem) => item[header.primaryKey] !== id)
            );
        } catch (error) {
            console.error(error);
            alert("Delete failed :(");
        }
    };

    if (!header || !header.content) {
        return <div className="error-label">Missing or invalid header configuration</div>;
    }

    return (
        <table className="table">
            <thead>
                <tr className="table-row">
                    <th className="table-cell">ID</th>
                    {header.content.map((property) => (
                        <th key={property.key} className="table-cell">
                            {property.label}
                        </th>
                    ))}
                    <th className="table-cell">CONFIG</th>
                </tr>
            </thead>
            <tbody>
                {loading ? (
                    <tr>
                        <td colSpan={header.content.length + 2} className="table-cell">
                            <label className="status-label warning-label">Loading...</label>
                        </td>
                    </tr>
                ) : error ? (
                    <tr>
                        <td colSpan={header.content.length + 2} className="table-cell">
                            <label className="status-label error-label">{error.message}</label>
                        </td>
                    </tr>
                ) : !data || data.length === 0 ? (
                    <tr>
                        <td colSpan={header.content.length + 2} className="table-cell">
                            <label className="status-label error-label">No data</label>
                        </td>
                    </tr>
                ) : (
                    data.map((item, itemIndex) => (
                        <tr key={`row${itemIndex}`} className="table-row">
                            <td className="table-cell">{item[header.primaryKey]}</td>
                            {header.content.map((property, propertyIndex) => (
                                <td key={`row${itemIndex}.${propertyIndex}`} className="table-cell">
                                    {renderCell(item, property)}
                                </td>
                            ))}
                            <td className="table-cell">
                                <a className="button">✏️Edit</a>
                                <a className="button button--danger" onClick={() => deleteData(Number(item[header.primaryKey]))}>
                                    🗑️Delete
                                </a>
                            </td>
                        </tr>
                    ))
                )}
            </tbody>
        </table>
    );
};

// Helper function to render cell content
const renderCell = (item: Record<string, any>, property: { type: string; key: string; label: string }) => {
    switch (property.type) {
        case "string":
        case "number":
            return item[property.key];
        case "boolean":
            return (
                <input
                    type="checkbox"
                    checked={Boolean(item[property.key])}
                    disabled={true}
                    title={property.label}
                    aria-label={property.label}
                />
            );
        case "image":
            return item[property.key] ? (
                <img
                    alt="image"
                    className="table-image"
                    src={`${domain}/${encodeURIComponent(item[property.key])}`}
                />
            ) : null;
        default:
            return "unsupported type";
    }
};

export default DataTable;
