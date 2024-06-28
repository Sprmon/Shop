declare global {
  namespace NodeJS {
    interface ProcessEnv {
      CATALOG_API: string;
      VERCEL?: string;
    }
  }
}

export const getServerSideConfig = () => {
  return {
    catalogApi: process.env.CATALOG_API,
    isVercel: !!process.env.VERCEL,
  }
};