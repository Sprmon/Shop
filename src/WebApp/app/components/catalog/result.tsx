'use client';
import useSWR from "swr";

import { CatalogItemPage } from "./item";
import styles from "./result.module.scss";
import { CatalogResult } from "@/lib/model/catalog";
import { ResponseError } from "@/lib/model/response";

function getAllCatalogItemsUrl(
  page?: number,
  pageSize?: number,
  brandId?: number,
  itemTypeId?: number) {
  let filter = "";
  if (itemTypeId !== undefined) {
    const brandValue = brandId !== undefined ? `${brandId}` : "";
    filter += `/type/${itemTypeId}/brand/${brandValue}`;
  } else if (brandId !== undefined) {
    filter += `/type/all/brand/${brandId}`;
  }
  return `/api/catalog/items${filter}?page=${page ?? 0}&pageSize=${pageSize ?? 10}`;
}

const fetcher = async (url: string) => {
  const res = await fetch(url);
  if (res.status != 200) {
    throw new Error("An error occurred while fetching the data.");
  }
  return res.json();
}

function getVisiblePageIndices(result: any) {
  return [1];
}

export function CatalogResultPage() {
  const { data, error, isLoading, isValidating } = useSWR<
    CatalogResult,
    ResponseError
  >(getAllCatalogItemsUrl(0, 10, 0, 0), fetcher);

  if (!data) {
    return <div>Loading...</div>;
  }

  const pageIndices = getVisiblePageIndices(data);

  return (
    <div>
      <div className={styles["catalog-items"]}>
        {
          data.data.map((item) => (
            <CatalogItemPage key={item.id} item={item} />
          ))
        }
      </div>

      <div className={styles["page-links"]}>
        {
          pageIndices.map((pageIndex) => (
            <a key={pageIndex} href="#">{pageIndex}</a>
          ))
        }
      </div>
    </div>
  )
}