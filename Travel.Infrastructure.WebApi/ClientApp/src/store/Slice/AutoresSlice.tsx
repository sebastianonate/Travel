import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import AutorInterface from '../../Interfaces/AutoresInterface'

export interface AutoresState {
  AutorSelected: AutorInterface | null
  modalForm: Boolean
  edit: Boolean
}

const initialState: AutoresState = {
  AutorSelected: null,
  modalForm: false,
  edit: false
}

export const AutoresSlice = createSlice({
  name: 'Autores',
  initialState,
  reducers: {
    selectAutor: (state, action: PayloadAction<AutorInterface | null>) => {
      state.AutorSelected = action.payload
    },
    setModalForm: (state, action: PayloadAction<Boolean>) => {
      state.modalForm = action.payload
    },
    setEditAutor: (state, action: PayloadAction<Boolean>) => {
      state.edit = action.payload
    }
  },
})

// Action creators are generated for each case reducer function
export const { selectAutor, setModalForm, setEditAutor } = AutoresSlice.actions

export default AutoresSlice.reducer