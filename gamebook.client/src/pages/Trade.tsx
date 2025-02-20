import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { domain } from "../utils";
import { Shops } from "../types";
import { GameContext } from "../context/GameContext";
import styles from "../styles/trade.module.css";

const TradePage = () => {
  const { id } = useParams();

  const [shop, setShop] = useState<Shops>();

  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error("GameContext is undefined");
  }
  const { inventory, setInventory, money, setMoney, lastLocation } = gameContext;

  useEffect(() => {
    const FetchData = async () => {
      try {
        const response = await fetch(
          `${domain}/api/Trades/ByInteractibleId/${id}`
        );
        const data = await response.json();
        setShop(data);
        console.log(data.trades);
      } catch (error) {
        console.log(error);
      }
    };
    FetchData();
  }, [id]);

  const isInInvent = (itemID: number) => {
    return inventory.some((item) => item.itemID === itemID);
  };

  const getInteractible = (intId: number) => {
    if(shop?.buys) {
        const interactible = shop.buys.find((buy) => buy.interactible.interactibleID === intId);
        return interactible ? interactible.interactible : null;
    }
    else if(shop?.trades) {
        const interactible = shop.trades.find((trade) => trade.interactible.interactibleID === intId);
        return interactible ? interactible.interactible : null;
    }
    
  }

  console.log(getInteractible(Number(id)))
  return (
    <>
    <div className={styles.tradePage} style={{backgroundImage: `url(data:image/webp;base64,${lastLocation?.backgroundImageBase64})`, backgroundSize: "cover"}}>
        <div className={styles.left}>
            <Link className={styles.def} to={`/Game/${lastLocation.locationID}`}>Zpět</Link>
        {/* <p>{getInteractible(Number(id))?.name}</p> */}
        <img className={styles.interactible}
                      src={`data:image/webp;base64,${getInteractible(Number(id))?.imageBase64}`}
                      alt={getInteractible(Number(id))?.name}
                    />
                    </div>
    <div className={styles.shop}>
    <p className={styles.def}>{`$ ${money}`}</p>
    {shop?.buys && <h2 className={styles.def}>Výměny</h2>}
      {shop?.trades.map((trades, index) => (
        <>
        <div
          key={index}
          className={`${styles.trades} ${
            isInInvent(trades.trade.item1.itemID)
              ? styles.canBuy
              : styles.cantBuy
          }`}
          onClick={() => {
            if (isInInvent(trades.trade.item1.itemID)) {
                setInventory((prevInventory) => {
                    const itemIndex = prevInventory.findIndex(
                        (item) => item.itemID === trades.trade.item1.itemID
                    );
                    if (itemIndex !== -1) {
                        const newInventory = [...prevInventory];
                        newInventory.splice(itemIndex, 1);
                        return newInventory.concat(trades.trade.item2);
                    }
                    return prevInventory;
                });
            }
          }}
        >
          <p>{trades.trade.item1.name}</p>
          <p>za</p>
          <p>{trades.trade.item2.name}</p>
        </div>
        </>
      ))}
        {shop?.buys && <h2 className={styles.def}>Nákupy</h2>}
      {shop?.buys.map((buys, index) => (
        <>
        <div
          key={index}
          className={
            money >= buys.item.tradeValue ? styles.canBuy : styles.cantBuy
          }
          onClick={() => {
            if (money >= buys.item.tradeValue) {
              setMoney((prevMoney: number) => prevMoney - buys.item.tradeValue);
            setInventory((prevInventory) => [...prevInventory, buys.item]);
            }
          }}
        >
          <p>
            {buys.item.name}...{buys.item.tradeValue}
          </p>
        </div>
        </>
      ))}
    </div>
    
    </div>
    </>
  );
};

export default TradePage;
