import Dropdown from "../components/Dropdown";
import DataTable from "../components/DataTable";
import { useState } from "react";
import { EndpointHeaderMap } from "../types/admin";
import { domain } from "../utils";
import PostPart from "../components/PostPart";

const Admin = () => {
    const [endpoint, setEndpoint] = useState<string>("/api/Locations");

    return (
        <>
            <div className="leftmenu">
                <h3>Admin</h3>
                <Dropdown title="Locations">
                    <a onClick={() => setEndpoint("/api/Locations")}>
                        Locations
                    </a>
                    <a onClick={() => setEndpoint("/api/LocationContent")}>
                        LocationContent
                    </a>
                    <a onClick={() => setEndpoint("/api/LocationPath")}>
                        LocationPaths
                    </a>
                </Dropdown>
                <Dropdown title="Items">
                    <a onClick={() => setEndpoint("/api/Items")}>Items</a>
                    <a onClick={() => setEndpoint("/api/ItemCategory")}>Category</a>
                    <a onClick={() => setEndpoint("/api/ConsumableItem")}>
                        Consumable
                    </a>
                </Dropdown>
                <Dropdown title="Interactibles">
                    <a onClick={() => setEndpoint("/api/Interactible")}>
                        Interactibles
                    </a>
                    <a onClick={() => setEndpoint("/api/InteractiblesItem")}>
                        InteractiblesItems
                    </a>
                    <a onClick={() => setEndpoint("/api/DataInteractiblesOption")}>
                        InteractibleOptions
                    </a>
                    <a onClick={() => setEndpoint("/api/OptionsEnum")}>
                        OptionsEnum
                    </a>
                    <Dropdown title="NPCs">
                        <a onClick={() => setEndpoint("/api/Dialog")}>
                            NpcDialog
                        </a>
                        <a
                            onClick={() =>
                                setEndpoint("/api/DialogResponse")
                            }
                        >
                            NpcDialogResponses
                        </a>
                    </Dropdown>
                </Dropdown>
            </div>
            <div style={{ marginLeft: "300px" }}>
                <h1>ADMIN</h1>
                <PostPart endpoint={endpoint}/>
                <DataTable
                    endpoint={`${domain}${endpoint}`}
                    header={EndpointHeaderMap[endpoint]}
                />
            </div>
        </>
    );
};
export default Admin;
