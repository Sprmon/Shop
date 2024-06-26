import styles from './search.module.scss'

export default function CatalogSearch(props: {
  catalogBrands: string[];
  catalogItemTypes: string[];
} = {
    catalogBrands: [],
    catalogItemTypes: [],
  }) {
  const catalogBrands = props.catalogBrands ?? [];
  const catalogItemTypes = props.catalogItemTypes ?? [];

  if (catalogBrands.length + catalogItemTypes.length === 0) {
    return (<></>)
  }

  return (
    < div className = { styles["search"]} >
      <div className={styles["search-header"]}>
        <img role='presentation' src='/images/filters.svg' />
        Filters
      </div>
      <div className={styles["search-type"]}>
        <div className={styles["search-group"]}>
          <h3>Brand</h3>
          <div className={styles["search-group-tags"]}>
            <a href='#' className={styles["search-tag"]}>All</a>
            {
              catalogBrands.map((brand) => (
                <a key={brand} href='#' className={styles["search-tag"]}>{brand}</a>
              ))
            }
          </div>
        </div>
        <div className={styles["search-group"]}>
          <h3>Type</h3>
          <div className={styles["search-group-tags"]}>
            <a href="#" className={styles["search-tag"]}>All</a>
            {
              catalogItemTypes.map((type) => (
                <a key={type} href="#" className={styles["search-tag"]}>{type}</a>
              ))
            }
          </div>
        </div>
      </div>
    </div >
  )
}