"use client";

import useSWR from "swr";

import fetcher from "@/lib/utils/fetcher";
import Screen from "@/components/layout/screen";
import { ResponseError } from "@/lib/model/response";
import { CatalogItem } from "@/lib/model/catalog";
import ItemDetail from "@/components/catalog/detail";

export default function Item({ params }: {
  params: {
    id: string;
  };
}) {
  const { data, error, isLoading } = useSWR<
    CatalogItem,
    ResponseError
  >(`/api/catalog/items/${params.id}?api-version=1.0`, fetcher);

  return (
    <Screen
      title={data?.name ?? "Loading..."}
      subtitle={data?.catalogBrand.brand ?? "Loading..."}
      element={<ItemDetail item={data} />}
    />
  )
}