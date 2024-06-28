import FooterLogo from '../../images/logo-footer.svg';
import styles from "./footer.scss";

export function Footer(props: {

}) {
  return (
    <footer>
      <div className={styles["footer-content"]}>
        <div className={styles["footer-row"]}>
          <FooterLogo />
          <p>© Sprmon Studio</p>
        </div>
      </div>
    </footer>
  )
}
