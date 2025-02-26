import { useContext, useEffect, useState } from "react";
import { GameContext } from "../context/GameContext";
import { FaTimes, FaBox } from "react-icons/fa";
import { ConsumableItem, Weapon, Item } from "../types";
import { domain } from "../utils";

import styles from "../styles/inventory.module.css";

const Inventory: React.FC = () => {
    const gameContext = useContext(GameContext);
    const [isVisible, setIsVisible] = useState(false);

    const [consumables, setConsumables] = useState<ConsumableItem[]>([]);
    const [weapons, setWeapons] = useState<Weapon[]>([]);

    useEffect(() => {
        const handleStorageChange = () => {
            const updatedInventory = JSON.parse(
                localStorage.getItem("inventory") || "[]"
            );
            setInventory(updatedInventory);
        };

        window.addEventListener("storage", handleStorageChange);

        return () => {
            window.removeEventListener("storage", handleStorageChange);
        };
    }, []);

    if (!gameContext) {
        return <div>Error: Game context is not available.</div>;
    }

    const {
        hp,
        defaultHp,
        setHp,
        equipedWeapon,
        setEquipedWeapon,
        energy,
        setEnergy,
        defaultEnergy,
        inventory,
        setInventory,
        resetEquipedWeapon,
    } = gameContext;

    useEffect(() => {
        const fetchTypes = async () => {
            const response = await fetch(
                `${domain}/api/Items/Consumables&Weapons`
            );
            const data = await response.json();

            setWeapons(data.weapons);
            setConsumables(data.consumables);
        };
        fetchTypes();
    }, []);

    const findConsumable = (id: number) => {
        return consumables.find(
            (consumable: ConsumableItem) => consumable.itemID === id
        );
    };

    const findWeapon = (id: number) => {
        return weapons.find((weapon: Weapon) => weapon.item.itemID === id);
    };

    const heal = (consumable: ConsumableItem) => {
        if (hp + consumable.healthValue < defaultHp) {
            setHp(hp + consumable.healthValue);
        } else {
            setHp(defaultHp);
        }
        if (energy + consumable.energyValue < defaultEnergy) {
            setEnergy(energy + consumable.energyValue);
        } else {
            setEnergy(defaultEnergy);
        }
    };

    const handleToggleInventory = () => {
        setIsVisible(!isVisible);
        if (!isVisible) {
            const updatedInventory = JSON.parse(
                localStorage.getItem("inventory") || "[]"
            );
            setInventory(updatedInventory);
            const updatedEquipedWeapon = JSON.parse(
                localStorage.getItem("equipedWeapon") || "[]"
            );
            setEquipedWeapon(updatedEquipedWeapon);
        }
    };

    return (
        <>
            <button onClick={handleToggleInventory}>
                {isVisible ? <FaTimes /> : <FaBox />}
            </button>
            {isVisible && (
                <div className={styles.inventory}>
                    <div
                        className={styles.container}
                        style={{ flexGrow: 1, overflowY: "auto" }}
                    >
                        <h2>Inventory</h2>
                        <ul>
                            {inventory.map((item, index) => (
                                <li
                                    key={index}
                                    className={styles.inventoryItem}
                                    onClick={() => {
                                        const updatedInventory =
                                            inventory.filter(
                                                (invItem, invIndex) =>
                                                    invIndex !== index
                                            );

                                        const consumable = findConsumable(
                                            item.itemID
                                        );
                                        if (consumable) {
                                            heal(consumable);
                                            setInventory(updatedInventory);
                                        } else if (findWeapon(item.itemID)) {
                                            setInventory(updatedInventory);
                                            setInventory(
                                                (prevInventory: Item[]) => [
                                                    ...prevInventory,
                                                    ...(equipedWeapon
                                                        ? [equipedWeapon.item]
                                                        : []),
                                                ]
                                            );
                                            setEquipedWeapon(
                                                findWeapon(item.itemID)
                                            );
                                        }
                                    }}
                                >
                                    <p>{item.name}</p>
                                    <span>
                                        {findConsumable(item.itemID) &&
                                            findConsumable(item.itemID)
                                                ?.energyValue != 0 && (
                                                <p>
                                                    {
                                                        findConsumable(
                                                            item.itemID
                                                        )?.energyValue
                                                    }
                                                    ⚡
                                                </p>
                                            )}
                                        {findConsumable(item.itemID) &&
                                            findConsumable(item.itemID)
                                                ?.healthValue != 0 && (
                                                <p>
                                                    {
                                                        findConsumable(
                                                            item.itemID
                                                        )?.healthValue
                                                    }
                                                    ❤️
                                                </p>
                                            )}
                                        {findWeapon(item.itemID) && (
                                            <p>
                                                {
                                                    findWeapon(item.itemID)
                                                        ?.damage
                                                }
                                                ⚔️
                                            </p>
                                        )}
                                    </span>
                                </li>
                            ))}
                        </ul>
                    </div>
                    <div className={styles.container}>
                        <h2>Equiped weapon</h2>
                        {equipedWeapon && (
                            <li
                                className={styles.inventoryItem}
                                onClick={() => {
                                    setInventory((prevInventory: Item[]) => [
                                        ...prevInventory,
                                        ...(equipedWeapon
                                            ? [equipedWeapon.item]
                                            : []),
                                    ]);
                                    resetEquipedWeapon();
                                }}
                            >
                                <p>{equipedWeapon?.name}</p>
                                <span>
                                    <p>
                                        {equipedWeapon.damage}
                                        ⚔️
                                    </p>
                                </span>
                            </li>
                        )}
                    </div>
                </div>
            )}
        </>
    );
};

export default Inventory;
