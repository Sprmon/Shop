
import styles from './header.module.scss';

export function Header(props: {
  isCatalog: boolean;
} = {
  isCatalog: false,
}) {
  return (
    <div className={`${styles["header"]} ${props.isCatalog ? styles["home"] : ""}`}>
      <div className={styles["header-hero"]}>

      </div>
      <div className={styles["header-container"]}>
        <nav className={styles["header-navbar"]}>
          <a className={`logo ${styles["logo-header"]}`} href="">
          </a>

        </nav>
        <div className={styles["header-intro"]}>
        </div>
      </div>
    </div>
  )
}
