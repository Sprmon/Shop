"use client";

import Catalog from "@/components/catalog"
import Screen from "@/components/layout/screen"

export default function SubCatalog({
  params
}: {
  params: {
    typeId: string,
    brandId: string,
  }
}) {
  const typeId = params.typeId === '0' ? undefined : Number(params.typeId);
  const brandId = params.brandId === '0' ? undefined : Number(params.brandId);

  return (
    <Screen
      title="Ready for a new adventure?"
      subtitle="Start the season with the latest in clothing and equipment."
      isCatalog={true}
      element={<Catalog brandId={brandId} itemTypeId={typeId} />}
    />
  )
}
