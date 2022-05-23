import {
  Container,
  createStyles,
  Grid,
  Input,
  makeStyles,
  TextField,
  Theme,
} from "@material-ui/core";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { useDispatch, useSelector } from "react-redux";
import FormModal from "../../Components/FormModal";
import { apiAutores } from "../../services";
import {
  setEditAutor,
  setModalForm,
} from "../../store/Slice/AutoresSlice";
import { RootState } from "../../store/store";

export interface AutoresFormProps {
  refetch: any;
  selectEntity: any;
}

interface IFormInput {
  nombre: string;
  apellidos: string;
}
const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      padding: theme.spacing(10),
    },
    title: {
      color: theme.palette.success.dark,
    },
  })
);

const AutoresForm: React.SFC<AutoresFormProps> = ({
  refetch,
  selectEntity,
}) => {
  const classes = useStyles();
  const dispatch = useDispatch();

  // const [ addSede, {isLoading: isLoadingAddSede} ] = useAddSedeMutation();
  // const [ editSede, {isLoading: isLoadingEditSede} ] = useEditSedeMutation();
  const AutorSelected = useSelector(
    (state: RootState) => state.autores.AutorSelected
  );
  const edit = useSelector((state: RootState) => state.autores.edit);
  const { control, handleSubmit } = useForm<IFormInput>();

  const onSubmit: SubmitHandler<IFormInput> = async (data) => {
    if (edit) {
      const id = AutorSelected?.id;
      if (id) {
        await apiAutores.update(id, {autorId: id, ...data});
        dispatch(setEditAutor(false));
      }
    } else {
      await apiAutores.create(data);
    }
    refetch();
    dispatch(setModalForm(false));
  };

  return (
    <Container>
      <form onSubmit={handleSubmit(onSubmit)}>
        <FormModal
          handleClose={() => {
            dispatch(setModalForm(false));
            dispatch(selectEntity(null));
          }}
          title="Nuevo Autor"
          loading={false}
        >
          <Grid>
            <Grid item>
              <Controller
                name="nombre"
                control={control}
                defaultValue={AutorSelected?.nombre}
                render={({ field }) => (
                  <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="email"
                    label="Nombre"
                    placeholder="Nombre"
                    autoFocus
                    {...field}
                  />
                )}
              />
            </Grid>
            <Grid item>
              <Controller
                name="apellidos"
                control={control}
                defaultValue={AutorSelected?.apellidos}
                render={({ field }) => (
                  <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="email"
                    label="Apellidos"
                    placeholder="Apellidos"
                    autoFocus
                    {...field}
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

export default AutoresForm;
