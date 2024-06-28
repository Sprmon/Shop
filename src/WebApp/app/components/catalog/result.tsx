'use client';

import useSWR from "swr";
import { useState } from "react";

import { CatalogItemPage } from "./item";
import styles from "./result.module.scss";
import { CatalogResult } from "@/lib/model/catalog";
import { ResponseError } from "@/lib/model/response";
import fetcher from "@/lib/utils/fetcher";
import { useSearchParams } from "next/navigation";

function getPagedUrl(pageIndex: number, pageSize: number, brandId?: number, itemTypeId?: number) {
  brandId = brandId ?? 0;
  itemTypeId = itemTypeId ?? 0;
  return `/catalog/${itemTypeId}/${brandId}?page=${pageIndex}&pageSize=${pageSize}`;
}

function gettemsUrl(page?: number, pageSize?: number, brandId?: number, itemTypeId?: number) {
  const itemTypeIdStr = (itemTypeId === undefined || itemTypeId === 0) ? "all" : itemTypeId.toString();
  const brandIdStr = (brandId === undefined || brandId === 0) ? "" : brandId.toString();
  const search = `?pageIndex=${page ?? 0}&pageSize=${pageSize ?? 10}&api-version=1.0`;
  return `/api/catalog/items/type/${itemTypeIdStr}/brand/${brandIdStr}${search}`;
}

function getVisiblePageIndices(result: CatalogResult) {
  const pageCount = Math.ceil(result.count / result.pageSize);
  return Array.from({ length: pageCount }, (_, i) => i + 1);
}

export function CatalogResultPage(props: {
  brandId: number | undefined,
  itemTypeId: number | undefined
}) {
  const searchParams = useSearchParams();
  const pageIndex = parseInt(searchParams.get("page") ?? "1");
  const pageSize = parseInt(searchParams.get("pageSize") ?? "10");
  console.log(pageIndex, pageSize);
  const brandId = props.brandId;
  const itemTypeId = props.itemTypeId;

  const { data, error, isLoading } = useSWR<
    CatalogResult,
    ResponseError
  >(gettemsUrl(pageIndex - 1, pageSize, brandId, itemTypeId), fetcher);

  if (!data) {
    return <div>Loading...</div>;
  }

  const pageIndices = getVisiblePageIndices(data);

  return (
    <div className={styles["catalog"]}>
      <div className={styles["catalog-items"]}>
        {
          data.data.map((item) => (
            <CatalogItemPage key={item.id} item={item} />
          ))
        }
      </div>

      <div className={styles["page-links"]}>
        {
          pageIndices.map((index) => (
            <a key={index} href={getPagedUrl(index, pageSize, brandId, itemTypeId)}>{index}</a>
          ))
        }
      </div>
    </div>
  )
}