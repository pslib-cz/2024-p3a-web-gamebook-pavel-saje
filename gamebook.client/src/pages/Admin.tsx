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
                <h3>Admin :)</h3>
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
                    <a
                        onClick={() =>
                            setEndpoint("https://localhost:7092/api/Category")
                        }
                    >
                        Category
                    </a>
                    <a
                        onClick={() =>
                            setEndpoint("https://localhost:7092/api/Consumable")
                        }
                    >
                        Consumable
                    </a>
                </Dropdown>
                <Dropdown title="Interactibles">
                    <a
                        onClick={() =>
                            setEndpoint(
                                "https://localhost:7092/api/Interactibles"
                            )
                        }
                    >
                        Interactibles
                    </a>
                    <a
                        onClick={() =>
                            setEndpoint(
                                "https://localhost:7092/api/InteractiblesOptions"
                            )
                        }
                    >
                        InteractiblesOptions
                    </a>
                    <a
                        onClick={() =>
                            setEndpoint(
                                "https://localhost:7092/api/OptionsEnum"
                            )
                        }
                    >
                        OptionsEnum
                    </a>
                    <Dropdown title="NPCs">
                        <a
                            onClick={() =>
                                setEndpoint(
                                    "https://localhost:7092/api/NpcDialog"
                                )
                            }
                        >
                            NpcDialog
                        </a>
                        <a
                            onClick={() =>
                                setEndpoint(
                                    "https://localhost:7092/api/NpcDialogResponses"
                                )
                            }
                        >
                            NpcDialogResponses
                        </a>
                    </Dropdown>
                </Dropdown>
            </div>
            <div style={{ marginLeft: "300px" }}>
                <h1>ADMIN</h1>
                <DataTable endpoint={endpoint} />
            </div>
        </>
    );
};
export default Admin;
