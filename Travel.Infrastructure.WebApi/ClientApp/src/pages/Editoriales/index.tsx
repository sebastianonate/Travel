import React, { useEffect, useState } from "react";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import ListCrud from "../../Components/ListCrud";
import { Delete, Edit } from "@material-ui/icons";
import { useDispatch, useSelector } from "react-redux";
import {
  selectEditorial,
  setEditEditorial,
  setModalForm,
} from "../../store/Slice/EditorialesSlice";
import { RootState } from "../../store/store";
import { EditorialesInterface} from "../../Interfaces";
import { apiEditoriales } from "../../services";
import { Mensagge } from "../../Components/Mensagge";
import EditorialesForm from "./EditorialesForm";

interface Column {
  id: string;
  label: string;
  minWidth?: number;
  align?: "right";
  format?: (value: number) => string;
  optionsRender?: (value: any) => JSX.Element;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: "100%",
    },
    container: {
      maxHeight: 440,
    },
    buttonEdit: {
      color: theme.palette.info.main,
      margin: theme.spacing(1),
      cursor: "pointer",
    },
    buttonDelete: {
      color: theme.palette.error.main,
      margin: theme.spacing(1),
      cursor: "pointer",
    },
  })
);

export default function Editoriales() {
  const classes = useStyles();
  const modalFormCoordinador = useSelector(
    (state: RootState) => state.editoriales.modalForm
  );
  const dispatch = useDispatch();

  const [page, setPage] = useState(0);
  const [total, setTotal] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);

  const [rows, setRows] = useState<EditorialesInterface[] | undefined>([]);
  const handleChangePage = async (
    event: React.ChangeEvent<HTMLInputElement>,
    page: number
  ) => {
    await setPage(Number(page));
  };
  const handleChangeRowsPerPage = async (
    event: React.ChangeEvent<HTMLInputElement>
  ) => {
    setRowsPerPage(Number(event.target.value));
    setPage(0);
  };

  useEffect(() => {
    (async () => {
      await actualizarTable();
    })();
  }, [page, rowsPerPage]);
  
  const actualizarTable = async () => {
    const editoriales = await apiEditoriales.getEntries() as EditorialesInterface[];
    setRows(editoriales)
  };
  
  const editar = async (id: any) => {
    const sedeEdit = await apiEditoriales.getEntry(id);
    if (sedeEdit) {
      await dispatch(selectEditorial(sedeEdit));
      await dispatch(setEditEditorial(true));
      await dispatch(setModalForm(true));
    }
  };
  
  const eliminar = async (id: any) => {
    const res = await apiEditoriales.remove(id);
    if(!res.succeeded){
      Mensagge(res.message,'', 'error')
    }else{
      await actualizarTable();
    }
  };
  const columns: Column[] = [
    { id: "nombre", label: "Nombre" },
    { id: "sede", label: "Sede" },
    {
      id: "options",
      label: "Opciones",
      optionsRender: (value: any) => (
        <>
          <Edit className={classes.buttonEdit} onClick={() => editar(value)} />
          <Delete
            className={classes.buttonDelete}
            onClick={() => eliminar(value)}
          />
        </>
      ),
    },
  ];

  return (
    <>
      <ListCrud
        statusModalForm={modalFormCoordinador}
        handleModalForm={setModalForm}
        columns={columns}
        rows={rows}
        handleChangePage={handleChangePage}
        handleChangeRowsPerPage={handleChangeRowsPerPage}
        title="Editoriales"
        selectEntity={selectEditorial}
        modalForm={<EditorialesForm selectEntity={selectEditorial} refetch={actualizarTable} />}
      />
    </>
  );
}
