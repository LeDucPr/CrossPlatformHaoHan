import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import fetchBooksNew from '../../fetchData/FetchBookNew';
import fetchCoverDatas from '../../fetchData/FetchCoverByListId';



//Tạo action FetchBookRanking
export const FetchBooksIdsNew = createAsyncThunk(
    'users/fetchByNewBookIdStatus',
    async ({ amount, skipIds }) => {
        const BooksNewIds = await fetchBooksNew(amount, skipIds);
        return BooksNewIds
    },
)

export const FetchBooksNew = createAsyncThunk(
    'users/fetchByNewBooksStatus',
    async (_, { getState }) => {
      const state = getState();
      const BooksNew = await fetchCoverDatas(state.newList.ListNewIds);
      return BooksNew;
    }
  );



//Khởi tạo trạng thái(state)
const initialState = {
    ListNewIds: [],
    ListNewbooks: [],
    skipIdslenght: 1,
    skipIds: [],
    isLoading: true,
    isReachEnd: true,
    isError: false,
}
//Tạo Slice từ redux
const newSlice = createSlice({
    name: 'newList',
    initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
        setReachEndTrueNew(state)  {
            state.isReachEnd = true;
        },
        setReachEndFalseNew(state)  {
            state.isReachEnd = false;
        },
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder
            .addCase(FetchBooksIdsNew.pending, (state, action) => {
            })
            .addCase(FetchBooksIdsNew.fulfilled, (state, action) => {
                state.ListNewIds = action.payload;
                state.skipIds = [...state.skipIds, ...action.payload];
                state.skipIdslenght+=5;
                state.isReachEnd = false;
            })
            .addCase(FetchBooksIdsNew.rejected, (state, action) => {
            })
            .addCase(FetchBooksNew.pending, (state, action) => {
                state.isError = false
            })
            .addCase(FetchBooksNew.fulfilled, (state, action) => {
                state.ListNewbooks = [...state.ListNewbooks, ...action.payload];
                state.isLoading = false
                // state.isError = false
            })
            .addCase(FetchBooksNew.rejected, (state, action) => {
                state.isLoading = false
                state.isError = true
            })
    },
})

export const {setReachEndTrueNew, setReachEndFalseNew} = newSlice.actions
export default newSlice.reducer;