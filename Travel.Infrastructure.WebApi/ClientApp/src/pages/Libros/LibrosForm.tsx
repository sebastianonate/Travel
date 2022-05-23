import {
  Checkbox,
  Container,
  createStyles,
  FormControl,
  Grid,
  Input,
  InputLabel,
  ListItemText,
  makeStyles,
  MenuItem,
  Select,
  TextField,
  Theme,
} from "@material-ui/core";
import { useEffect, useState } from "react";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import FormModal from "../../Components/FormModal";
import { AutoresInterface, EditorialesInterface } from "../../Interfaces";
import { apiAutores, apiEditoriales, apiLibros } from "../../services";
import {
  setEditLibro,
  setModalForm,
} from "../../store/Slice/LibrosSlice";
import { RootState } from "../../store/store";
import Autocomplete from '@material-ui/lab/Autocomplete';
import { Mensagge } from "../../Components/Mensagge";

export interface LibrosFormProps {
  refetch: any;
  selectEntity: any;
}

interface IFormInput {
  titulo: string;
  sinopsis: string;
  noPaginas: string;
  editorialId: number | undefined;
  autores: AutoresInterface[] ;
}
const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      padding: theme.spacing(10),
    },
    title: {
      color: theme.palette.success.dark,
    },
    formControl: {
      margin: theme.spacing(1),
      minWidth: 120,
    },
  })
);

const LibrosForm: React.SFC<LibrosFormProps> = ({
  refetch,
  selectEntity,
}) => {
  const classes = useStyles();
  const dispatch = useDispatch();
  const [Editoriales, setEditoriales] = useState<EditorialesInterface[]>([]);
  const [Autores, setAutores] = useState<AutoresInterface[]>([]);
  
  const LibroSelected = useSelector(
    (state: RootState) => state.libros.LibroSelected
    );
  const [autoresSelect, setAutoresSelect] = useState<AutoresInterface[] | undefined>(LibroSelected?.autores);
  const edit = useSelector((state: RootState) => state.libros.edit);
  const { control, handleSubmit } = useForm<IFormInput>();

  useEffect(() => {
    (async () => {
      const editoriales = await apiEditoriales.getEntries();
      setEditoriales(editoriales)
      const autores = await apiAutores.getEntries();
      setAutores(autores)
    })();
  }, []);

  const onSubmit: SubmitHandler<IFormInput> = async (data) => {
    console.log(data)
    if (edit) {
      const id = LibroSelected?.id;
      if (id) {
        const form = {...data, autores: data.autores?.map(d => ({autorId: d.id}))}
        const res = await apiLibros.update(id, { libroId: id, ...form });
        if(!res.succeeded){
          Mensagge(res.message, '','error')
        }else{
          dispatch(setEditLibro(false));
          dispatch(setModalForm(false));
          refetch();
        }
      }
    } else {
      const form = {...data, autores: autoresSelect?.map(d => ({autorId: d.id}))}
      const res = await apiLibros.create(form);
      if(!res.succeeded){
        Mensagge(res.message, '','error')
      }else{
        dispatch(setModalForm(false));
        refetch();
      }

    }
  };

  return (
    <Container>
      <form onSubmit={handleSubmit(onSubmit)}>
        <FormModal
          handleClose={() => {
            dispatch(setModalForm(false));
            dispatch(selectEntity(null));
          }}
          title="Nuevo Libro"
          loading={false}
        >
          <Grid>
            <Grid item>
              <Controller
                name="titulo"
                control={control}
                defaultValue={LibroSelected?.titulo}
                render={({ field }) => (
                  <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="titulo"
                    label="Titulo"
                    placeholder="Titulo"
                    autoFocus
                    {...field}
                  />
                )}
              />
            </Grid>
            <Grid item>
              <Controller
                name="sinopsis"
                control={control}
                defaultValue={LibroSelected?.sinopsis}
                render={({ field }) => (
                  <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="sinopsis"
                    label="Sinopsis"
                    placeholder="Sinopsis"
                    autoFocus
                    {...field}
                  />
                )}
              />
            </Grid>
            <Grid item>
              <Controller
                name="noPaginas"
                control={control}
                defaultValue={LibroSelected?.noPaginas}
                render={({ field }) => (
                  <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="email"
                    label="Numero de paginas"
                    placeholder="Numero de paginas"
                    autoFocus
                    {...field}
                  />
                )}
              />
            </Grid>
            <Grid item>
              <Controller
                name="editorialId"
                control={control}
                defaultValue={LibroSelected?.editorial.id}
                render={({ field }) => (
                  <FormControl  variant="outlined" className={classes.formControl}>
                    <InputLabel id="demo-simple-select-filled-label">
                    Editorial
                    </InputLabel>
                    <Select {...field}>
                      {Editoriales.map((d) => (
                        <MenuItem value={d.id}>{d.nombre}</MenuItem>
                      ))}
                    </Select>
                  </FormControl>
                )}
              />
            </Grid>
            <Grid item>
              <Controller
                name="autores"
                control={control}
                defaultValue={LibroSelected?.autores}
                render={({ field }) => (
                  <Autocomplete
                    multiple
                    id="tags-standard"
                    options={Autores}
                    getOptionLabel={(option) => option.nombre}
                    {...field}
                    onChange = {(event, value: AutoresInterface[]) => setAutoresSelect(value)}
                    getOptionSelected = {(option, value) => option.id === value.id }
                    renderInput={(params) => (
                      <TextField
                        {...params}
                        variant="standard"
                        label="Autores"
                        placeholder="Autores"
                      />
                    )}
                  />
                )}
              />
            </Grid>
          </Grid>
        </FormModal>
      </form>
    </Container>
  );
};

export default LibrosForm;
