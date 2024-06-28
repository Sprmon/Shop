export const getBuildConfig = () => {
  const isApp = !!process.env.BUILD_APP;
  return {
    isApp,
  }
};

export type BuildConfig = ReturnType<typeof getBuildConfig>;
