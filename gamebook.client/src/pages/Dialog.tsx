import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Dialog } from "../types";
import { domain } from "../utils";
import { GameContext } from "../context/GameContext";
import styles from "../styles/dialog.module.css";

const DialogPage = () => {
  const { id } = useParams();

  const gameContext = useContext(GameContext);

  if (!gameContext) {
    return <div>Erro: GameContext</div>;
  }

  const { lastLocation } = gameContext;

  const [dialog, setDialog] = useState<Dialog>();
  const [error, setError] = useState<Error | null>(null);
  const [loading, setLoading] = useState<boolean>(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${domain}/api/DataDialog/${id}`);
        const json = await response.json();
        setDialog(json);
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
  }, [id]);

  return (
    <div
      style={{
        backgroundImage: `url(${domain}${lastLocation.backgroundImagePath})`,
        width: "100%",
        height: "100vh",
      }}
    >
      <img
        className={styles.interactible}
        src={`${domain}${dialog?.interactible.imagePath}`}
        alt={dialog?.interactible.name}
      />
      <div className={styles.box}>
        <h2>{dialog?.interactible.name}</h2>
        <p>{dialog?.text}</p>
        <div className={styles.interacts}>
          <div className={styles.buttons}>
            {dialog?.dialogResponses &&
              dialog.dialogResponses.map((response) => (
                <Link to={`/Dialog/${response.nextDialogID}`}>
                  {response.responseText}
                </Link>
              ))}
          </div>
          <div className={styles.buttons}>
            {dialog?.nextDialogID && (
              <Link to={`/Dialog/${dialog.nextDialogID}`}>Další</Link>
            )}
            <Link to={`/Game/${lastLocation.locationID}`}>Ukončit</Link>
          </div>
        </div>
      </div>
    </div>
  );
};

export default DialogPage;
