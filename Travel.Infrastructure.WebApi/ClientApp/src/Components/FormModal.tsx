import React from 'react';
import { Box, Button, Card, CardHeader, createStyles, makeStyles, Theme } from '@material-ui/core';

//Components

export interface FormModalProps {
  handleClose: any; 
  title: string; 
  loading: boolean;
}

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      paddingLeft: theme.spacing(10),
      paddingRight: theme.spacing(10),
      paddingBottom: theme.spacing(5),
      paddingTop: theme.spacing(5),
      margin: theme.spacing(10)
    },
    title: {
      color: theme.palette.success.dark,
    },
    form: {
      backgroundColor: "white",
    },
  })
);

const FormModal: React.SFC<FormModalProps> = ({handleClose, title, loading, children }) => {
  const classes = useStyles();
  return (
      <Card className={classes.root}>
        <h1 className={classes.title}>{title}</h1>
            {children}
        <Box
          display="flex"
          justifyContent="flex-end"
          p={2}
        >
          <Button
            color="inherit"
            variant="contained"
            style={{margin: '7px'}}
            onClick={()=>handleClose()}
            disabled={loading}
          >
            Cancelar
          </Button>
          <Button
              type="submit"
              color="primary"
              variant="contained"
              style={{margin: '7px'}}
              disabled={loading}
            >
              {
                loading ?
                'Guardando...'
                : 'Guardar' 
              }
            </Button>
        </Box>
      </Card>
  )
}

export default FormModal;