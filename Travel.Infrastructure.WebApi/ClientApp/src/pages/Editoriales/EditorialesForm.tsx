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
import { apiEditoriales } from "../../services";
import {
  setEditEditorial,
  setModalForm,
} from "../../store/Slice/EditorialesSlice";
import { RootState } from "../../store/store";

export interface AutoresFormProps {
  refetch: any;
  selectEntity: any;
}

interface IFormInput {
  nombre: string;
  sede: string;
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

const EditorialesForm: React.SFC<AutoresFormProps> = ({
  refetch,
  selectEntity,
}) => {
  const classes = useStyles();
  const dispatch = useDispatch();

  const EditorialSelected = useSelector(
    (state: RootState) => state.editoriales.EditorialSelected
  );
  const edit = useSelector((state: RootState) => state.editoriales.edit);
  const { control, handleSubmit } = useForm<IFormInput>();

  const onSubmit: SubmitHandler<IFormInput> = async (data) => {
    if (edit) {
      const id = EditorialSelected?.id;
      if (id) {
        await apiEditoriales.update(id, {editorialId: id, ...data});
        dispatch(setEditEditorial(false));
      }
    } else {
      await apiEditoriales.create(data);
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
          title="Nuevo Editorial"
          loading={false}
        >
          <Grid>
            <Grid item>
              <Controller
                name="nombre"
                control={control}
                defaultValue={EditorialSelected?.nombre}
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
                name="sede"
                control={control}
                defaultValue={EditorialSelected?.sede}
                render={({ field }) => (
                  <TextField
                    variant="outlined"
                    margin="normal"
                    required
                    fullWidth
                    id="email"
                    label="Sede"
                    placeholder="Sede"
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

export default EditorialesForm;
