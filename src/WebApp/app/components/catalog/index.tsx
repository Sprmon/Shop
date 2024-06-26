import dynamic from 'next/dynamic';

import styles from './index.module.scss'
import CatalogSearch from "./search";

import LoadingIcon from "@/icons/three-dots.svg";

function Loading() {
  return (
    <div className={styles["loading-content"] + " no-dark"}>
      <LoadingIcon />
    </div >
  )
}

export default function Catalog() {
  const CatalogResult = dynamic(async () => (await import('./result')).CatalogResultPage, {
    loading: () => <Loading />,
  });

  return (
    <main className={styles["catalog"]}>
      <CatalogSearch catalogBrands={["Apple"]} catalogItemTypes={[]} />
      <CatalogResult />
    </main>
  )
}
