import Dropdown from "../components/Dropdown";
import DataTable from "../components/DataTable";
import { useState } from "react";

const Admin = () => {
    const [endpoint, setEndpoint] = useState<string>(
        "https://localhost:7092/api/Locations"
    );
    return (
        <>
            <div className="leftmenu">
                <h3>this is sex</h3>
                <Dropdown title="Locations">
                    <a
                        onClick={() =>
                            setEndpoint("https://localhost:7092/api/Locations")
                        }
                    >
                        Locations
                    </a>
                    <a
                        onClick={() =>
                            setEndpoint(
                                "https://localhost:7092/api/LocationContent"
                            )
                        }
                    >
                        LocationContent
                    </a>
                    <a
                        onClick={() =>
                            setEndpoint(
                                "https://localhost:7092/api/LocationPaths"
                            )
                        }
                    >
                        LocationPaths
                    </a>
                </Dropdown>
                <Dropdown title="Items">
                    <a
                        onClick={() =>
                            setEndpoint("https://localhost:7092/api/Items")
                        }
                    >
                        Items
                    </a>
                    <a>bagr</a>
                    <a>kuřecí varlata</a>
                </Dropdown>
                <a>pohlavní styk s vopicí</a>
            </div>
            <div style={{ marginLeft: "300px" }}>
                <h1>SEXADMIN</h1>
                <DataTable endpoint={endpoint} />
            </div>
        </>
    );
};
export default Admin;
