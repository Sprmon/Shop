
export default async function fetcher(url: string) {
  const res = await fetch(url);

  if (res.status != 200) {
    throw new Error("An error occurred while fetching the data.");
  }
  return res.json();
};
