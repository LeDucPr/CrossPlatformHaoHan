import { configureStore } from '@reduxjs/toolkit'
import rankingReducer from './slices/rankingSlice'
import newReducer from './slices/newSlice'
import libraryReducer from './slices/librarySlice'
import suggestionReducer from './slices/suggestionSlice'
export const store = configureStore({
  reducer: {
    rankingList : rankingReducer,
    newList : newReducer,
    libraryList : libraryReducer,
    suggestionList : suggestionReducer, 
  },
})  