import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import fetchBooksGenre from '../../fetchData/FetchBooksGenre';
import fetchCoversDataFromFieldsContrains from '../../fetchData/FetchFromFields';
import fetchCoverDatas from '../../fetchData/FetchCoverByListId';


export const FetchBooksIdsGenres = createAsyncThunk(
    'users/fetchByGenresBookIdStatus',
    async ({ amount, genre, skipIds }) => {
        const BooksGenreIds = await fetchBooksGenre(amount, genre, skipIds);
        return BooksGenreIds
    },
)


export const FetchBooksGenres = createAsyncThunk(
    'users/fetchByGenresBooksStatus',
    async (_, { getState }) => {
        const state = getState();
        const BooksGenre = await fetchCoverDatas(state.genreList.ListGenresIds);
        return BooksGenre;
        
    }
);



//Khởi tạo trạng thái(state)
const initialState = {
    Genre: "Action",
    ListGenresIds: [],
    ListGenresBooks: [],
    isLoading: true,
    skipIds: [],
    isReachEnd: true,
    isError: false,
}
//Tạo Slice từ redux
const genresSlice = createSlice({
    name: 'genresList',
    initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
        setIsFetchAllFalseGenres(state) {
        },
        setGenre(state, action) {
            state.Genre = action.payload;
            state.ListGenresIds =[];
            state.ListGenresBooks = [];
            state.skipIds = [];
            state.isReachEnd = true;
            state.isLoading = true;
        },
        setReachEndTrueGenre(state)  {
            state.isReachEnd = true;
        },
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder
            .addCase(FetchBooksIdsGenres.pending, (state, action) => {
            })
            .addCase(FetchBooksIdsGenres.fulfilled, (state, action) => {
                state.ListGenresIds = action.payload;
                state.skipIds = [...state.skipIds, ...action.payload];
                state.isReachEnd = false;
            })
            .addCase(FetchBooksIdsGenres.rejected, (state, action) => {
            })
            .addCase(FetchBooksGenres.pending, (state, action) => {
                state.isError = false
            })
            .addCase(FetchBooksGenres.fulfilled, (state, action) => {
                state.ListGenresBooks = [...state.ListGenresBooks, ...action.payload];
                state.isLoading = false;
            })
            .addCase(FetchBooksGenres.rejected, (state, action) => {
            })
    },
})

export const { setGenre, setReachEndTrueGenre } = genresSlice.actions
export default genresSlice.reducer;