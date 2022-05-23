import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import LibroInterface from '../../Interfaces/LibrosInterface'

export interface LibrosState {
  LibroSelected: LibroInterface | null
  modalForm: Boolean
  edit: Boolean
}

const initialState: LibrosState = {
  LibroSelected: null,
  modalForm: false,
  edit: false
}

export const LibrosSlice = createSlice({
  name: 'Libros',
  initialState,
  reducers: {
    selectLibro: (state, action: PayloadAction<LibroInterface | null>) => {
      state.LibroSelected = action.payload
    },
    setModalForm: (state, action: PayloadAction<Boolean>) => {
      state.modalForm = action.payload
    },
    setEditLibro: (state, action: PayloadAction<Boolean>) => {
      state.edit = action.payload
    }
  },
})

// Action creators are generated for each case reducer function
export const { selectLibro, setModalForm, setEditLibro } = LibrosSlice.actions

export default LibrosSlice.reducer