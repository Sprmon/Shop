import FooterLogo from '../../images/logo-footer.svg';
import "./footer.css";

export function Footer(props: {

}) {
  return (
    <footer>
      <div className="footer-content">
        <div className="footer-row">
          <FooterLogo />
          <p>© Sprmon Studio</p>
        </div>
      </div>
    </footer>
  )
}
