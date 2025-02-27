import { useContext, useEffect, useState } from "react";
import { Link, useParams } from "react-router-dom";
import { Dialog } from "../types";
import { domain } from "../utils";
import { GameContext } from "../context/GameContext";
import styles from "../styles/dialog.module.css";
import StaggerText from "react-stagger-text";
import Loading from "../components/Loading";

const DialogPage = () => {
  const { id } = useParams();
  const [dialog, setDialog] = useState<Dialog>();
  const [loading, setLoading] = useState<boolean>(false);

  const gameContext = useContext(GameContext);

  if (!gameContext) {
    return <div>Erro: GameContext</div>;
  }

  const { lastLocation, radiation, hp } = gameContext;


  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${domain}/api/DataDialog/${id}`);
        const json = await response.json();
        setDialog(json);
      } catch (error) {
        console.error('Failed to fetch dialog:', error);
        setLoading(false);
      }
    };
    fetchData();
  }, [id]);

  return (
    <>
      {loading && <Loading/>}
      <div
        style={{
          backgroundImage: `url(${domain}/${encodeURIComponent(lastLocation?.backgroundImagePath)})`,
          backgroundSize: "cover",
          width: "100%",
          height: "100vh",
        }}
      >
        {/* <img
          className={styles.interactible}
          src={`${domain}${dialog?.interactible.imagePath}`}
          alt={dialog?.interactible.name}
        /> */}
        {dialog?.interactible.imagePath &&
          <img
            style={{ width: "100%", height: "100vh" }}
            src={`${domain}/${encodeURIComponent(dialog?.interactible.imagePath || '')}`}
            alt={dialog?.interactible.name}
          />
        }
        <div className={styles.box}>
          <h2>{dialog?.interactible.name}</h2>
          {/* TODO */}
          {typeof dialog?.text === "string" && (
            <StaggerText
              staggerType="letter"
              staggerEasing="ease"
              staggerDuration={0.000001}
              startDelay={0.004}
            >
              {dialog.text}
            </StaggerText>
          )}
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
              <Link to={lastLocation.end != null || radiation>=100 || hp<=0 ? "/" : `/Game/lastLocation.locationID`}>Ukončit</Link>
            </div>
          </div>
        </div>
      </div>
    </>
  );
};

export default DialogPage;
