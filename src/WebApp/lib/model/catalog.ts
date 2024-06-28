
export type CatalogBrand = {
  id: number;
  brand: string;
};

export type CatalogItemType = {
  id: number;
  type: string;
};

export type CatalogItem = {
  id: number;
  name: string;
  description: string;
  price: number;
  pictureUri: string;
  pictureFileName: string;
  catalogBrandId: number;
  catalogBrand: CatalogBrand;
  catalogTypeId: number;
  catalogType: CatalogItemType;
};

export type CatalogResult = {
  pageIndex: number;
  pageSize: number;
  count: number;
  data: CatalogItem[];
};
