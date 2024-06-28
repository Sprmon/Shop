/** @type {import('next').NextConfig} */
const mode = process.env.BUILD_MODE ?? "standalone";
console.log("[Next] build mode", mode);

const disableChunk = (mode === "export");
console.log("[Next] build with chunk: ", !disableChunk);

const nextConfig = {
  webpack(config) {
    config.module.rules.push({
      test: /\.svg$/,
      use: ["@svgr/webpack"],
    });

    if (disableChunk) {
      config.plugins.push(
        new webpack.optimize.LimitChunkCountPlugin({ maxChunks: 1 }),
      );
    }

    config.resolve.fallback = {
      child_process: false,
    };

    return config;
  },
  images: {
    unoptimized: mode === "export",
  },
  output: mode,
  reactStrictMode: true,
};

const CorsHeaders = [
  { key: "Access-Control-Allow-Credentials", value: "true" },
  { key: "Access-Control-Allow-Origin", value: "*" },
  {
    key: "Access-Control-Allow-Methods",
    value: "*",
  },
  {
    key: "Access-Control-Allow-Headers",
    value: "*",
  },
  {
    key: "Access-Control-Max-Age",
    value: "86400",
  },
];

if (mode !== "export") {
  nextConfig.headers = async () => {
    return [
      {
        source: "/api/:path*",
        headers: CorsHeaders,
      },
    ];
  };

  let rewriteRules = [];
  if (process.env.CATALOG_API) {
    rewriteRules.push({
      source: "/api/catalog/:path*",
      destination: `${process.env.CATALOG_API}/api/catalog/:path*`,
    });
  }

  nextConfig.rewrites = async () => {
    return {
      beforeFiles: rewriteRules,
    };
  };
}

export default nextConfig;
