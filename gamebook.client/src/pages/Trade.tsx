import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { domain } from "../utils";
import { Shops } from "../types";
import { GameContext } from "../context/GameContext";
import styles from "../styles/trade.module.css";
import Loading from "../components/Loading";
import LandscapeWarning from "../components/LandscapeWarning";
import { useOrientation } from "../hooks/useOrientation";

const TradePage = () => {
  const isLandscape = useOrientation();
  const { id } = useParams();
  const [shop, setShop] = useState<Shops | undefined>(undefined);
  const [loading, setLoading] = useState<boolean>(true);

  const gameContext = useContext(GameContext);
  if (!gameContext) {
    throw new Error("GameContext is undefined");
  }
  const { inventory, setInventory, money, setMoney, lastLocation, InteractiblesRemovedFromLocation } = gameContext;

  useEffect(() => {
    if (!isLandscape) {
      setLoading(false);
      return;
    }

    const FetchData = async () => {
      try {
        const response = await fetch(
          `${domain}/api/Trades/ByInteractibleId/${id}`
        );
        const data = await response.json();
        setShop(data);
        console.log(data);
      } catch (error) {
        console.log(error);
      } finally {
        setLoading(false);
      }
    };
    FetchData();
  }, [id, isLandscape]);

  const isInInvent = (itemID: number) => {
    return inventory.some((item) => item.itemID === itemID);
  };

  console.log(InteractiblesRemovedFromLocation)

  const getInteractible = (intId: number) => {
    if (shop?.buys) {
      const interactible = shop.buys.find((buy) => buy.interactible.interactibleID === intId);
      return interactible ? interactible.interactible : null;
    }else if (shop?.sells) {
      const interactible = shop.sells.find((sell) => sell.interactible.interactibleID === intId);
      return interactible ? interactible.interactible : null; 
    } else if (shop?.trades) {
      const interactible = shop.trades.find((trade) => trade.interactible.interactibleID === intId);
      return interactible ? interactible.interactible : null;
    } else if (shop?.tradeInteractibles) {
      const interactible = shop.tradeInteractibles.find((trade) => trade.interactible.interactibleID === intId);
      return interactible ? interactible.interactible : null; 
    } 
    return null;
  };

  if (!isLandscape) {
    return <LandscapeWarning />;
  }

  if (loading) {
    return <Loading />;
  }

  return (
    <div className={styles.tradePage}>
      <div className={styles.left}>
        <Link className={styles.def} to={`/Game/${lastLocation.locationID}`}>
          Zpět
        </Link>
        {getInteractible(Number(id)) && (
          <img
            className={styles.interactible}
            src={`${domain}/Uploads/${getInteractible(Number(id))?.imagePath.replace(/\\/g, "/")}`}
            alt={getInteractible(Number(id))?.name}
          />
        )}
      </div>
      <div className={styles.shop}>
        <p className={styles.def}>{`$ ${money}`}</p>
        {shop?.trades && shop.trades.length > 0 && (
          <>
            <h2 className={styles.def}>Výměny</h2>
            {shop.trades.map((trades, index) => (
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
            ))}
          </>
        )}
        {shop?.tradeInteractibles && shop.tradeInteractibles.length > 0 && (
          <>
            <h2 className={styles.def}>Úkolové věci</h2>
            {shop.tradeInteractibles.map((trades, index) => (
                <div
                key={index}
                className={
                  InteractiblesRemovedFromLocation.find(
                    (removed) => removed.interactibleID === trades.interactibleID
                  )
                    ? styles.canBuy
                    : styles.cantBuy
                }
                onClick={() => {
                  console.log(trades)
                  if (InteractiblesRemovedFromLocation.find((removed) => removed.interactibleID === trades.interactibleID)) {
                    setInventory((prevInventory) => [
                      ...prevInventory,
                      trades.item,
                    ]);
                  }
                }}
                >
                <p>{trades.text}</p>
                </div>
            ))}
          </>
        )}
        {shop?.buys && shop.buys.length > 0 && (
          <>
            <h2 className={styles.def}>Nákupy</h2>
            {shop.buys.map((buys, index) => (
              <div
              key={index}
              className={
                money >= buys.item.tradeValue ? styles.canBuy : styles.cantBuy
              }
              onClick={() => {
                if (money >= buys.item.tradeValue) {
                  setMoney(
                    (prevMoney: number) => prevMoney - buys.item.tradeValue
                  );
                  setInventory((prevInventory) => [
                    ...prevInventory,
                    buys.item,
                  ]);
                }
              }}
              >
                <p>
                  {buys.item.name}...{buys.item.tradeValue}
                </p>
              </div>
            ))}
          </>
        )}
        {shop?.sells && shop.sells.length > 0 && (
          <>
            <h2 className={styles.def}>Výkupy</h2>
            {shop.sells.map((sell, index) => (
              <div
                key={index}
                className={`${styles.trades} ${
                  isInInvent(sell.item.itemID)
                    ? styles.canBuy
                    : styles.cantBuy
                }`}
                onClick={() => {
                  if (isInInvent(sell.item.itemID)) {
                    setInventory((prevInventory) => {
                      const itemIndex = prevInventory.findIndex(
                        (item) => item.itemID === sell.item.itemID
                      );
                      if (itemIndex !== -1) {
                        const newInventory = [...prevInventory];
                        newInventory.splice(itemIndex, 1);
                        return newInventory;
                      }
                      return prevInventory;
                    });
                    setMoney((prevMoney) => prevMoney + sell.item.tradeValue);
                  }
                }}
              >
                <p>{sell.item.name}...{sell.item.tradeValue}</p>
              </div>
            ))} 
          </>
        )}
      </div>
    </div>
  );
};

export default TradePage;
