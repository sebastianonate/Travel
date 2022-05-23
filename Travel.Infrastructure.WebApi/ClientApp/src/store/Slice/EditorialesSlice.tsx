import { createSlice, PayloadAction } from '@reduxjs/toolkit'
import EditorialInterface from '../../Interfaces/EditorialesInterface'

export interface EditorialesState {
  EditorialSelected: EditorialInterface | null
  modalForm: Boolean
  edit: Boolean
}

const initialState: EditorialesState = {
  EditorialSelected: null,
  modalForm: false,
  edit: false
}

export const EditorialesSlice = createSlice({
  name: 'Editoriales',
  initialState,
  reducers: {
    selectEditorial: (state, action: PayloadAction<EditorialInterface | null>) => {
      state.EditorialSelected = action.payload
    },
    setModalForm: (state, action: PayloadAction<Boolean>) => {
      state.modalForm = action.payload
    },
    setEditEditorial: (state, action: PayloadAction<Boolean>) => {
      state.edit = action.payload
    }
  },
})

// Action creators are generated for each case reducer function
export const { selectEditorial, setModalForm, setEditEditorial } = EditorialesSlice.actions

export default EditorialesSlice.reducer