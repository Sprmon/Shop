import FilterIcon from '@/icons/filters.svg';
import styles from './search.module.scss';
import { CatalogBrand, CatalogItemType } from '@/lib/model/catalog';

function getSearchUrl(brandId: number | undefined, typeId: number | undefined) {
  if (brandId === undefined && typeId === undefined) {
    return "#";
  }
  if (brandId === undefined) {
    return `/catalog/${typeId}`;
  }
  if (typeId === undefined) {
    return `/catalog/0/${brandId}`;
  }
  return `/catalog/${typeId}/${brandId}`;
}

export default function CatalogSearch(props: {
  catalogBrands?: CatalogBrand[];
  catalogItemTypes?: CatalogItemType[];
  currentBrandId?: number | undefined;
  currentTypeId?: number | undefined;
}) {
  const catalogBrands = props.catalogBrands ?? [];
  const catalogItemTypes = props.catalogItemTypes ?? [];

  if (catalogBrands.length + catalogItemTypes.length === 0) {
    return (<></>)
  }

  return (
    <div className={styles["search"]} >
      <div className={styles["search-header"]}>
        <FilterIcon />
        Filters
      </div>
      <div className={styles["search-type"]}>
        <div className={styles["search-group"]}>
          <h3>Brand</h3>
          <div className={styles["search-group-tags"]}>
            <a
              href={getSearchUrl(undefined, props.currentTypeId)}
              className={styles["search-tag"]}>All</a>
            {
              catalogBrands.map((v) => (
                <a
                  key={v.id}
                  href={getSearchUrl(v.id, props.currentTypeId)}
                  className={styles["search-tag"]}
                >
                  {v.brand}
                </a>
              ))
            }
          </div>
        </div>
        <div className={styles["search-group"]}>
          <h3>Type</h3>
          <div className={styles["search-group-tags"]}>
            <a
              href={getSearchUrl(props.currentBrandId, undefined)}
              className={styles["search-tag"]}
            >
              All
            </a>
            {
              catalogItemTypes.map((v) => (
                <a
                  key={v.id}
                  href={getSearchUrl(props.currentBrandId, v.id)}
                  className={styles["search-tag"]}
                >
                  {v.type}
                </a>
              ))
            }
          </div>
        </div>
      </div>
    </div >
  )
}