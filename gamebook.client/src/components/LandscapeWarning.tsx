import styles from '../styles/landscapeWarning.module.css';

const LandscapeWarning = () => {
  return (
    <div className={styles.warning}>
      <div className={styles.phone}>
      </div>
        <div className={styles.message}>
          Prosím, otočte své zařízení
        </div>
    </div>
  );
};

export default LandscapeWarning;
