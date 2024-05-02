import { configureStore } from '@reduxjs/toolkit'
import rankingReducer from './slices/rankingSlice'

export const store = configureStore({
  reducer: {
    rankingList : rankingReducer
  },
})  