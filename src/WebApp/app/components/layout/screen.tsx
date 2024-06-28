"use client";

import React, { useEffect, useState } from "react";
import { Provider } from "react-redux";

import { store } from "@/lib/store";
import { getClientConfig } from "@/lib/config/client";
import { Header, Footer, ErrorBoundary } from "@/components/layout";
import LoadingIcon from "@/icons/three-dots.svg";
import { usePathname } from "next/navigation";

export function Loading() {
  return (
    <div className="loading-content no-dark">
      <LoadingIcon />
    </div>
  );
}

function useHasHydrated() {
  const [hasHydrated, setHasHydrated] = useState<boolean>(false);

  useEffect(() => {
    setHasHydrated(true);
  }, []);
  return hasHydrated;
}

export default function Screen({
  element,
  title,
  subtitle,
  isCatalog,
}: Readonly<{
  element: React.ReactNode;
  title: string;
  subtitle: string;
  isCatalog?: boolean;
}>) {
  const pathname = usePathname();

  if (!useHasHydrated()) {
    return <Loading />;
  }

  return (
    <Provider store={store}>
      <ErrorBoundary>
        <Header isCatalog={!!isCatalog} title={title} subtitle={subtitle} />
        <main className="container">
          {element}
        </main>
        <Footer />
      </ErrorBoundary>
    </Provider>
  )
}
