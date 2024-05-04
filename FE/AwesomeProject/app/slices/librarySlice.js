import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import getUserLibrary from '../../fetchData/FetchUserLibrary';
import fetchCoverDatas from '../../fetchData/FetchCoverByListId';



//Tạo action FetchBookLibrary
export const FetchBooksIdsLibrary = createAsyncThunk(
    'users/fetchByLibraryBookIdStatus',
    async ({ clientId }) => {
        const BooksLibraryIds = await getUserLibrary(clientId);
        return BooksLibraryIds
    },
)

export const FetchBooksLibrary = createAsyncThunk(
    'users/fetchByLibraryBooksStatus',
    async (_, { getState }) => {
      const state = getState();
      const reversedListBookId = [...state.libraryList.ListLibraryIds].reverse();
      const BooksLibrary = await fetchCoverDatas(reversedListBookId);
      return BooksLibrary;
    }
  );



//Khởi tạo trạng thái(state)
const initialState = {
    ListLibraryIds: [],
    ListLibraryBooks: [],
    isLoading: false,
    isFetchAll: false,
    isError: false,
}
//Tạo Slice từ redux
const librarySlice = createSlice({
    name: 'libraryList',
    initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
        setIsFetchAllFalseLibrary(state)  {
            state.isFetchAll = false;
            state.ListLibraryIds = [];
        },
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder
            .addCase(FetchBooksIdsLibrary.pending, (state, action) => {
                state.isLoading = true;
                state.isError = false
            })
            .addCase(FetchBooksIdsLibrary.fulfilled, (state, action) => {
                state.ListLibraryIds = action.payload;
            })
            .addCase(FetchBooksIdsLibrary.rejected, (state, action) => {
                state.isError = true
            })
            .addCase(FetchBooksLibrary.pending, (state, action) => {
                state.isError = false
            })
            .addCase(FetchBooksLibrary.fulfilled, (state, action) => {
                state.ListLibraryBooks = action.payload;
                state.isLoading = false
                state.isFetchAll = true
                state.isError = false
            })
            .addCase(FetchBooksLibrary.rejected, (state, action) => {
                state.isLoading = false
                state.isError = true
            })
    },
})

export const {setIsFetchAllFalseLibrary} = librarySlice.actions
export default librarySlice.reducer;