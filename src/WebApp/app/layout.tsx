import type { Viewport, Metadata } from "next";
import { Inter } from "next/font/google";
import { SpeedInsights } from "@vercel/speed-insights/next";

import "./globals.css";
import "./layout.scss";
import { getClientConfig } from "@/lib/config/client";
import { getServerSideConfig } from "@/lib/config/server";

const inter = Inter({ subsets: ["latin"] });

export const viewport: Viewport = {
  width: "device-width",
  initialScale: 1,
  maximumScale: 1,
}

export const metadata: Metadata = {
  title: "Sprmon Shop",
  description: "A simple e-commerce site",
  appleWebApp: {
    title: "Sprmon Shop",
    statusBarStyle: "default",
  },
};

export default function RootLayout({
  children,
}: Readonly<{
  children: React.ReactNode;
}>) {
  const serverConfig = getServerSideConfig();

  return (
    <html>
      <head>
        <meta name="config" content={JSON.stringify(getClientConfig())} />
        <link rel="manifest" href="/site.webmanifest"></link>
      </head>
      <body className={inter.className}>
        {children}
        {
          serverConfig?.isVercel && (
            <><SpeedInsights /></>
          )
        }
      </body>
    </html>
  );
}
