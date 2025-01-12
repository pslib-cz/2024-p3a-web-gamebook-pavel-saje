import Dropdown from "../components/Dropdown";
import DataTable from "../components/DataTable";
import { useState, useContext } from "react";
import { EndpointHeaderMap } from "../types/admin";
import { domain } from "../utils";

import { GameContext } from "../context/GameContext";

const Admin = () => {
    const [endpoint, setEndpoint] = useState<string>("/api/Locations");

    const gameContext = useContext(GameContext);

    if (!gameContext) {
        throw new Error("GameContext is undefined");
    }

    const { resetInventory } = gameContext;
    const { resetMoney } = gameContext;
    const { resetRadiation } = gameContext;
    const { resetEnergy } = gameContext;
    const { resetHp } = gameContext;
    const { resetInteractiblesRemovedFromLocation } = gameContext;

    return (
        <>
            <div className="leftmenu">
                <h3>Admin :)</h3>
                <Dropdown title="Locations">
                    <a onClick={() => setEndpoint("/api/Locations")}>
                        Locations
                    </a>
                    <a onClick={() => setEndpoint("/api/LocationContent")}>
                        LocationContent
                    </a>
                    <a onClick={() => setEndpoint("/api/LocationPaths")}>
                        LocationPaths
                    </a>
                </Dropdown>
                <Dropdown title="Items">
                    <a onClick={() => setEndpoint("/api/Items")}>Items</a>
                    <a onClick={() => setEndpoint("/api/Category")}>Category</a>
                    <a onClick={() => setEndpoint("/api/Consumable")}>
                        Consumable
                    </a>
                </Dropdown>
                <Dropdown title="Interactibles">
                    <a onClick={() => setEndpoint("/api/Interactibles")}>
                        Interactibles
                    </a>
                    <a onClick={() => setEndpoint("/api/InteractibleOptions")}>
                        InteractibleOptions
                    </a>
                    <a onClick={() => setEndpoint("/api/OptionsEnum")}>
                        OptionsEnum
                    </a>
                    <Dropdown title="NPCs">
                        <a onClick={() => setEndpoint("/api/NpcDialog")}>
                            NpcDialog
                        </a>
                        <a
                            onClick={() =>
                                setEndpoint("/api/NpcDialogResponses")
                            }
                        >
                            NpcDialogResponses
                        </a>
                    </Dropdown>
                </Dropdown>
                <Dropdown title="stats">
                <a onClick={() => resetHp()}>Reset HP</a>
                <a onClick={() => resetEnergy()}>Reset Energy</a>
                <a onClick={() => resetRadiation()}>Reset Radiation</a>
                <a onClick={() => resetMoney()}>Reset Money</a>
                <a onClick={() => {resetEnergy(); resetHp(); resetMoney(); resetRadiation();}}>Reset All</a>
            </Dropdown>
            <Dropdown title="Interactibles">
            <a onClick={() => resetInventory()}>Reset Inventory</a>
            <a onClick={() => resetInteractiblesRemovedFromLocation()}>Reset RemovedInteractibles</a>
            <a onClick={() => {resetInventory(); resetInteractiblesRemovedFromLocation();}}>Reset All</a>
            </Dropdown>
            </div>
            <div style={{ marginLeft: "300px" }}>
                <h1>ADMIN</h1>
                <DataTable
                    endpoint={`${domain}${endpoint}`}
                    header={EndpointHeaderMap[endpoint]}
                />
            </div>
        </>
    );
};
export default Admin;
