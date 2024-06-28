import CartIcon from "@/icons/cart.svg";
import styles from "./cart.module.scss";

export function CartMenu(props: {
  count: number;
}) {
  return (
    <a aria-label="cart" href="cart">
      <CartIcon />
      {
        props.count > 0 && (<span className={styles["cart-badge"]}>{ props.count }</span>)
      }
    </a>
  )
}
