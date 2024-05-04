import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import getUserSuggestion from '../../fetchData/FetchClientSuggestionBook';
import fetchCoverDatas from '../../fetchData/FetchCoverByListId';



//Tạo action FetchBookSuggestion
export const FetchBooksIdsSuggestion = createAsyncThunk(
    'users/fetchBySuggestionBookIdStatus',
    async ({ clientId }) => {
        const BooksSuggestionIds = await getUserSuggestion(clientId);
        return BooksSuggestionIds.slice(0,8)
    },
)

export const FetchBooksSuggestion = createAsyncThunk(
    'users/fetchBySuggestionBooksStatus',
    async (_, { getState }) => {
      const state = getState();
      const BooksSuggestion = await fetchCoverDatas(state.suggestionList.ListSuggestionIds);
      return BooksSuggestion;
    }
  );



//Khởi tạo trạng thái(state)
const initialState = {
    ListSuggestionIds: [],
    ListSuggestionBooks: [],
    isLoading: false,
    isError: false,
    isFetchAll: false,
}
//Tạo Slice từ redux
const suggestionSlice = createSlice({
    name: 'suggestionList',
    initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
        setIsFetchAllFalseSuggestion(state)  {
            state.ListSuggestionIds = [];
        },
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder
            .addCase(FetchBooksIdsSuggestion.pending, (state, action) => {
                state.isFetchAll = true;
                state.isLoading = true;
                state.isError = false
            })
            .addCase(FetchBooksIdsSuggestion.fulfilled, (state, action) => {
                state.ListSuggestionIds = action.payload;
            })
            .addCase(FetchBooksIdsSuggestion.rejected, (state, action) => {
                state.isError = true
            })
            .addCase(FetchBooksSuggestion.pending, (state, action) => {
                state.isError = false
                state.isFetchAll = false;
            })
            .addCase(FetchBooksSuggestion.fulfilled, (state, action) => {
                state.ListSuggestionBooks = action.payload;
                state.isLoading = false
                state.isError = false
                state.isFetchAll = true;
            })
            .addCase(FetchBooksSuggestion.rejected, (state, action) => {
                state.isLoading = false
                state.isError = true
            })
    },
})

export const {setIsFetchAllFalseSuggestion} = suggestionSlice.actions
export default suggestionSlice.reducer;