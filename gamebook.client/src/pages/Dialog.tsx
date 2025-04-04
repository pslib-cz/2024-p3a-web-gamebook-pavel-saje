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

  const processedId = id?.split("&")[0];
  const init = id?.split("&")[1] ? id?.split("&")[1] : false

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await fetch(`${domain}/api/Dialog/${processedId}&${init}`);
        const json = await response.json();
        setDialog(json);
      } catch (error) {
        console.error('Failed to fetch dialog:', error);
        setLoading(false);
      }
    };
    fetchData();
  }, [id]);

  if (!gameContext) {
    return <div>Erro: GameContext</div>;
  }

  const { lastLocation, radiation, hp } = gameContext;

  return (
    <>
      {loading && <Loading/>}
      <main className={styles.dialog}
        style={{
          backgroundImage: `url(${domain}/Uploads/${lastLocation?.backgroundImagePath.replace(/\\/g, "/")})`,
          backgroundSize: "cover",
          width: "100%",
          height: "100vh",
        }}
      >
        {dialog?.interactible?.imagePath &&
          <img
            style={{ width: "100%", height: "100vh" }}
            src={`${domain}/Uploads/${dialog?.interactible.imagePath.replace(/\\/g, "/")}`}
            alt={dialog?.interactible.name}
          />
        }
        <div className={styles.box}>
          <span>
          <h2>{dialog?.interactible?.name}</h2>
          {/* TODO */}
          {typeof dialog?.text === "string" && (
            <StaggerText
              staggerType="letter"
              staggerEasing="ease"
              staggerDuration={0.01}
              startDelay={0.004}
            >
              {dialog.text}
            </StaggerText>
          )}
          <div className={styles.interacts}>
            <div className={styles.buttons}>
              {dialog?.dialogResponses &&
                dialog.dialogResponses.map((response) => (
                  <Link className={styles.link} to={`/Dialog/${response.nextDialogID}`}>
                    {response.responseText}
                  </Link>
                ))}
            </div>
            <div className={styles.buttons}>
              {dialog?.nextDialogID && (
                <Link className={styles.link} to={`/Dialog/${dialog.nextDialogID}`}>Další</Link>
              )}
              <Link className={styles.link} to={lastLocation.end != null || radiation>=100 || hp<=0 ? "/" : `/Game/${lastLocation.locationID}`}>Ukončit</Link>
            </div>
          </div>
          </span>
        </div>
      </main>
    </>
  );
};

export default DialogPage;
