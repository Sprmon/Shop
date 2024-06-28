import Image from 'next/image'

import { CartMenu } from './cart';
import HomeImage from '@/images/header-home.webp';
import HeaderImage from '@/images/header.webp';
import HeaderLogo from '@/images/logo-header.svg';
import styles from './header.module.scss';

export function Header(props: {
  isCatalog: boolean;
  title: string;
  subtitle: string;
}) {
  return (
    <div className={`${styles["header"]} ${props.isCatalog ? styles["catalog"] : ""}`}>
      <div className={styles["header-hero"]}>
        <Image
          src={props.isCatalog ? HomeImage : HeaderImage}
          role="presentation" alt="presentation" />
      </div>
      <div className={styles["header-container"]}>
        <nav className={styles["header-navbar"]}>
          <a className={`logo ${styles["logo-header"]}`} href="">
            <HeaderLogo />
          </a>
          <CartMenu count={1} />
        </nav>
        <div className={styles["header-intro"]}>
          <h1>{ props.title }</h1>
          <p>{ props.subtitle }</p>
        </div>
      </div>
    </div>
  )
}
