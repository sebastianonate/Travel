import React, { useEffect, useState } from "react";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import ListCrud from "../../Components/ListCrud";
import { Delete, Edit, Label } from "@material-ui/icons";
import { useDispatch, useSelector } from "react-redux";
import {
  selectLibro,
  setEditLibro,
  setModalForm,
} from "../../store/Slice/LibrosSlice";
import { RootState } from "../../store/store";
import { AutoresInterface, LibrosInterface} from "../../Interfaces";
import { apiLibros } from "../../services";
import { Mensagge } from "../../Components/Mensagge";
import LibrosForm from "./LibrosForm";

interface Column {
  id: string;
  label: string;
  minWidth?: number;
  align?: "right";
  format?: (value: number) => string;
  optionsRender?: (value: any) => JSX.Element;
  optionsArray?: (value: any) => JSX.Element;
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

export default function Libros() {
  const classes = useStyles();
  const modalFormLibro = useSelector(
    (state: RootState) => state.libros.modalForm
  );
  const dispatch = useDispatch();

  const [page, setPage] = useState(0);
  const [total, setTotal] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);

  const [rows, setRows] = useState<LibrosInterface[] | undefined>([]);
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
    const libros = await apiLibros.getEntries() as LibrosInterface[];
    setRows(libros)
  };
  
  const editar = async (id: any) => {
    const sedeEdit = await apiLibros.getEntry(id);
    if (sedeEdit) {
      await dispatch(selectLibro(sedeEdit));
      await dispatch(setEditLibro(true));
      await dispatch(setModalForm(true));
    }
  };
  
  const eliminar = async (id: any) => {
    const res = await apiLibros.remove(id);
    if(!res.succeeded){
      Mensagge(res.message,'', 'error')
    }else{
      await actualizarTable();
    }
  };
  const columns: Column[] = [
    { id: "titulo", label: "Titulo" },
    { id: "sinopsis", label: "Sinopsis" },
    { id: "noPaginas", label: "Numero de paginas" },
    { id: "editorial.nombre", label: "Editorial" },
    { 
      id: "autores", 
      label: "Autores",
      optionsArray: (value: AutoresInterface[]) => (
        <>
          {value.map(v => (
            <label>{`(${v.nombre} ${v.apellidos})`}</label>
          ))}
        </>
      ),
    },
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
        statusModalForm={modalFormLibro}
        handleModalForm={setModalForm}
        columns={columns}
        rows={rows}
        handleChangePage={handleChangePage}
        handleChangeRowsPerPage={handleChangeRowsPerPage}
        title="Libros"
        selectEntity={selectLibro}
        modalForm={<LibrosForm selectEntity={selectLibro} refetch={actualizarTable} />}
      />
    </>
  );
}
