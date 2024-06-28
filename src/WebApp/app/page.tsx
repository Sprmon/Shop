"use client";

import Catalog from "@/components/catalog";
import Screen from "@/components/layout/screen";

export default function Home() {
  return (
    <Screen
      title="Ready for a new adventure?"
      subtitle="Start the season with the latest in clothing and equipment."
      isCatalog={true}
      element={<Catalog />}
    />
  );
}
