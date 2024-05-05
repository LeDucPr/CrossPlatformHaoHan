import { createAsyncThunk, createSlice } from '@reduxjs/toolkit'
import fetchCoversDataFromFieldsContrains from '../../fetchData/FetchFromFields';
import fetchCoverDatas from '../../fetchData/FetchCoverByListId';



export const FetchBooksGenres = createAsyncThunk(
    'users/fetchByGenresBooksStatus',
    async (_, { getState }) => {
      const state = getState();
      const genre = state.genreList.Genre
      const amountWord = genre.length
      console.log(amountWord)
      const amountCovers = 9
      const skipIds = state.genreList.skipIds
      const fields = {
        genre : genre
      };
      const ListBooksGenres = await fetchCoversDataFromFieldsContrains(amountWord, amountCovers, skipIds, fields)
      console.log(ListBooksGenres[0])
      return 1;
    }
  );



//Khởi tạo trạng thái(state)
const initialState = {
    Genre: "Action",
    ListGenresIds: [],
    ListGenresBooks: [],
    isLoading: false,
    skipIds: [],
    isReachEnd: true,
    isError: false,
    Test: 0,
}
//Tạo Slice từ redux
const genresSlice = createSlice({
    name: 'genresList',
    initialState,
    reducers: {
        // standard reducer logic, with auto-generated action types per reducer
        setIsFetchAllFalseGenres(state)  {
        },
        setGenre(state, action){
            state.Genre = action.payload;
            console.log(state.Genre);
        }
    },
    extraReducers: (builder) => {
        // Add reducers for additional action types here, and handle loading state as needed
        builder
            .addCase(FetchBooksGenres.pending, (state, action) => {
                state.isReachEnd = false;
            })
            .addCase(FetchBooksGenres.fulfilled, (state, action) => {
                state.Test+=1
            })
            .addCase(FetchBooksGenres.rejected, (state, action) => {
            })
    },
})

export const {setGenre} = genresSlice.actions
export default genresSlice.reducer;