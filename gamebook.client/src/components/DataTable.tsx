import { useEffect, useState } from "react";
import { EndpointHeader } from "../types/admin";

type DataTableProps = {
    endpoint: string;
    header: EndpointHeader;
};

const DataTable: React.FC<DataTableProps> = ({ endpoint, header }) => {
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
                prevData.filter((item) => item[header.primaryKey] !== id)
            );
        } catch (error) {
            console.error(error);
            alert("Delete failed :(");
        }
    };

    return (
        <table className="table">
            <caption className="table-caption">
                <span>{endpoint.split("/").filter(Boolean).reverse()[0]}</span>
                <a className="button">+Add new</a>
            </caption>
            <thead>
                <tr className="table-row">
                    {header.content.length > 0 && (
                        <>
                            <th className="table-cell">ID</th>
                            {header.content.map((property) => (
                                <th key={property.key} className="table-cell">
                                    {property.label}
                                </th>
                            ))}
                            <th className="table-cell">CONFIG</th>
                        </>
                    )}
                </tr>
            </thead>
            <tbody>
                {data && data.length > 0 ? (
                    data.map((item, itemIndex) => (
                        <tr key={`row${itemIndex}`} className="table-row">
                            <td className="table-cell">
                                {item[header.primaryKey]}
                            </td>
                            {header.content.map((property, propertyIndex) => (
                                <td
                                    key={`row${itemIndex}.${propertyIndex}`}
                                    className="table-cell"
                                >
                                    {property.type == "string" ? (
                                        item[property.key]
                                    ) : property.type == "number" ? (
                                        item[property.key]
                                    ) : property.type == "boolean" ? (
                                        <input
                                            type="checkbox"
                                            value={item[property.key]}
                                            disabled={true}
                                        ></input>
                                    ) : (
                                        "unsupported type"
                                    )}
                                </td>
                            ))}
                            <td className="table-cell">
                                <a className="button">✏️Edit</a>
                                <a
                                    className="button button--danger"
                                    onClick={() =>
                                        deleteData(item[header.primaryKey])
                                    }
                                >
                                    🗑️Delete
                                </a>
                            </td>
                        </tr>
                    ))
                ) : loading ? (
                    <tr>
                        <td className="table-cell">Loading...</td>
                    </tr>
                ) : error ? (
                    <tr>
                        <td className="table-cell">{error.message}</td>
                    </tr>
                ) : (
                    <tr>
                        <td className="table-cell">smrdis prdis</td>
                    </tr>
                )}
            </tbody>
        </table>
    );
};

export default DataTable;
