import { configureStore } from '@reduxjs/toolkit'
import autoresReducer from "./Slice/AutoresSlice";
import editorialReducer from "./Slice/EditorialesSlice";
import libroReducer from "./Slice/LibrosSlice";
export const store = configureStore({
  reducer: {
     autores: autoresReducer,
     editoriales: editorialReducer,
     libros: libroReducer,
  },
})

// Infer the `RootState` and `AppDispatch` types from the store itself
export type RootState = ReturnType<typeof store.getState>
// Inferred type: {posts: PostsState, comments: CommentsState, users: UsersState}
export type AppDispatch = typeof store.dispatch