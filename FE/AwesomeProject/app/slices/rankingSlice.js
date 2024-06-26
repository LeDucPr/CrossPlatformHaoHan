import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import fetchBooksRanking from '../../fetchData/FetchBookRanking';
import fetchCoverDatas from '../../fetchData/FetchCoverByListId';



//Tạo action FetchBookRanking
export const FetchBooksIdsRanking = createAsyncThunk(
    'users/fetchByBookIdStatus',
    async ({ amount, skipIds }) => {
        const Booksranking = await fetchBooksRanking(amount, skipIds);
        return Booksranking
    },
)

export const FetchBooksRanking = createAsyncThunk(
    'users/fetchByBooksStatus',
    async (_, { getState }) => {
      const state = getState();
      const Booksranking = await fetchCoverDatas(state.rankingList.ListRankingIds);
      return Booksranking;
    }
  );



//Khởi tạo trạng thái(state)
const initialState = {
    ListRankingIds: [],
    ListRankingBooks: [],
    skipIdslenght: 1,
    skipIds: [],
    isLoading: false,
    isReachEnd: true,
    isError: false,
}
//Tạo Slice từ redux
const rankingSlice = createSlice({
    name: 'rankingList',
    initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
        setReachEndTrue(state)  {
            state.isReachEnd = true;
        },
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder
            .addCase(FetchBooksIdsRanking.pending, (state, action) => {
            })
            .addCase(FetchBooksIdsRanking.fulfilled, (state, action) => {
                state.ListRankingIds = action.payload;
                state.skipIds = [...state.skipIds, ...action.payload];
                state.skipIdslenght+=5;
                state.isReachEnd = false;
            })
            .addCase(FetchBooksIdsRanking.rejected, (state, action) => {
            })
            .addCase(FetchBooksRanking.pending, (state, action) => {
                state.isError = false
            })
            .addCase(FetchBooksRanking.fulfilled, (state, action) => {
                state.ListRankingBooks = [...state.ListRankingBooks, ...action.payload];
                state.isLoading = false
                // state.isError = false
            })
            .addCase(FetchBooksRanking.rejected, (state, action) => {
                state.isLoading = false
                state.isError = true
            })
    },
})

export const {setReachEndTrue, setReachEndFalse} = rankingSlice.actions
export default rankingSlice.reducer;