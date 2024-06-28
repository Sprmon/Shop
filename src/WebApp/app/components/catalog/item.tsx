import { CatalogItem } from "@/lib/model/catalog";
import styles from "./item.module.scss";

export function CatalogItemPage(props: {
  item: CatalogItem;
}) {
  let pictureUrl = props.item.pictureUri ?? "/images/catalog";
  pictureUrl = `${pictureUrl}/${props.item.pictureFileName}`

  return (
    <div className={styles["catalog-item"]}>
      <a className={styles["catalog-product"]} href={`/item/${props.item.id}`}>
        <span className={styles["catalog-product-image"]}>
          <img alt={props.item.name} src={pictureUrl} />
        </span>
        <span className={styles["catalog-product-content"]}>
          <span className={styles["name"]}>{ props.item.name }</span>
          <span className={styles["price"]}>{ props.item.price }</span>
        </span>
      </a>
    </div>
  )
}
