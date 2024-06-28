import { createAsyncThunk, createSlice } from "@reduxjs/toolkit";

import { RootState } from "@/lib/store";
import { CatalogBrand, CatalogItemType } from "@/lib/model/catalog";

interface CatalogBrandState {
  loading: string;
  error: string | null;
  values: CatalogBrand[];
}

interface CatalogTypeState {
  loading: string;
  error: string | null;
  values: CatalogItemType[];
}

interface CatalogState {
  brand: CatalogBrandState;
  type: CatalogTypeState;
};

const initialState: CatalogState = {
  brand: {
    loading: "idle",
    error: null,
    values: [],
  },
  type: {
    loading: "idle",
    error: null,
    values: [],
  },
};

export const fetchBrands = createAsyncThunk(
  "catalog/brand",
  async () => {
    const response = await fetch("/api/catalog/catalogbrands?api-version=1.0");
    return (await response.json()) as CatalogBrand[];
  },
  {
    condition: (_, { getState }) => {
      const state = getState() as RootState;
      if (state.catalog.brand.loading !== "idle") {
        return false;
      }
      return true;
    }
  }
)

export const fetchTypes = createAsyncThunk(
  "catalog/type",
  async () => {
    const response = await fetch("/api/catalog/catalogtypes?api-version=1.0");
    return (await response.json()) as CatalogItemType[];
  },
  {
    condition: (_, { getState }) => {
      const state = getState() as RootState;
      if (state.catalog.type.loading !== "idle") {
        return false;
      }
      return true;
    }
  }
)

export const brandSlice = createSlice({
  name: "catalog-brand",
  initialState,
  reducers: { },
  extraReducers: (builder) => {
    builder.addCase(fetchBrands.pending, (state) => {
      state.brand.loading = "pending";
    }).addCase(fetchBrands.fulfilled, (state, action) => {
      state.brand.values = action.payload;
      state.brand.loading = 'fulfilled';
    }).addCase(fetchTypes.pending, (state) => {
      state.type.loading = "pending";
    }).addCase(fetchTypes.fulfilled, (state, action) => {
      state.type.values = action.payload;
      state.type.loading = 'fulfilled';
    });
  },
});

export default brandSlice.reducer;
