import { useEffect, useState } from "react";
import { useAppDispatch } from '@/lib/store';
import { CatalogBrand, CatalogItemType } from "@/lib/model/catalog";
import { fetchBrands, fetchTypes } from "@/lib/store/features/catalog/catalogSlice";

import styles from './index.module.scss';
import CatalogSearch from "./search";
import { CatalogResultPage } from './result';

export default function Catalog(props: {
  brandId?: number | undefined,
  itemTypeId?: number | undefined,
}) {
  const [catalogBrands, setCatalogBrands] = useState<CatalogBrand[]>([]);
  const [catalogItemTypes, setCatalogItemTypes] = useState<CatalogItemType[]>([]);

  const dispatch = useAppDispatch();
  useEffect(() => {
    dispatch(fetchBrands()).then((value) => {
      if (fetchBrands.fulfilled.match(value)) {
        setCatalogBrands(value.payload);
      }
    });
    dispatch(fetchTypes()).then((value) => {
      if (fetchTypes.fulfilled.match(value)) {
        setCatalogItemTypes(value.payload);
      }
    });
  });


  return (
    <div className={styles["catalog"]}>
      <CatalogSearch
        catalogBrands={catalogBrands}
        catalogItemTypes={catalogItemTypes}
        currentBrandId={props.brandId}
        currentTypeId={props.itemTypeId}
      />
      <CatalogResultPage
        brandId={props.brandId}
        itemTypeId={props.itemTypeId}
      />
    </div>
  )
}
