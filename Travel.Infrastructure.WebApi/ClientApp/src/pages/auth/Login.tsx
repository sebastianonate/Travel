import React, { useState } from "react";
import Avatar from "@material-ui/core/Avatar";
import Button from "@material-ui/core/Button";
import CssBaseline from "@material-ui/core/CssBaseline";
import TextField from "@material-ui/core/TextField";
import FormControlLabel from "@material-ui/core/FormControlLabel";
import Checkbox from "@material-ui/core/Checkbox";
import Link from "@material-ui/core/Link";
import Paper from "@material-ui/core/Paper";
import Box from "@material-ui/core/Box";
import Grid from "@material-ui/core/Grid";
import Alert from '@material-ui/lab/Alert';
import LockOutlinedIcon from "@material-ui/icons/LockOutlined";
import Typography from "@material-ui/core/Typography";
import { makeStyles } from "@material-ui/core/styles";
import { Controller, SubmitHandler, useForm } from "react-hook-form";
import { ILogin } from "../../Interfaces";
import { apiIdentity } from "../../services";

export interface LoginProps {}

function Copyright() {
  return (
    <Typography variant="body2" color="textSecondary" align="center">
      {"Copyright © "}
      <Link color="inherit" href="https://material-ui.com/">
        Your Website
      </Link>{" "}
      {new Date().getFullYear()}
      {"."}
    </Typography>
  );
}

const useStyles = makeStyles((theme) => ({
  root: {
    height: "100vh",
  },
  image: {
    backgroundImage: "url(https://source.unsplash.com/random)",
    backgroundRepeat: "no-repeat",
    backgroundColor:
    theme.palette.type === "light"
    ? theme.palette.grey[50]
    : theme.palette.grey[900],
    backgroundSize: "cover",
    backgroundPosition: "center",
  },
  paper: {
    margin: theme.spacing(8, 4),
    display: "flex",
    flexDirection: "column",
    alignItems: "center",
  },
  avatar: {
    margin: theme.spacing(1),
    backgroundColor: theme.palette.secondary.main,
  },
  form: {
    width: "100%", // Fix IE 11 issue.
    marginTop: theme.spacing(1),
  },
  submit: {
    margin: theme.spacing(3, 0, 2),
  },
  alertError: {
    width: '100%'
  }
}));


export const Login: React.SFC<LoginProps> = () => {
  
  const [username, setusername] = useState('');
  const [pass, setpass] = useState('');
  const [error, seterror] = useState(false);
  
  const { control, handleSubmit } = useForm<ILogin>();
  const classes = useStyles();
  const onSubmit: SubmitHandler<ILogin> = async (data) => {
    try {
      const res = await apiIdentity.login(data);
      if(res.succeeded){
        localStorage.setItem('token', res.jwToken);
        window.location.reload()
      }else{
        seterror(true)
      }
    } catch (error) {
      seterror(true)
    }
    
  };
  return (
    <Grid container component="main" className={classes.root}>
      <CssBaseline />
      <Grid item xs={false} sm={4} md={8} lg={10} className={classes.image} />
      <Grid
        item
        xs={12}
        sm={8}
        md={4}
        lg={2}
        component={Paper}
        elevation={6}
        square
      >
        <div className={classes.paper}>
          <Avatar className={classes.avatar}>
            <LockOutlinedIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Iniciar sesion
          </Typography>
          {
            error && 
            <Alert severity="error" className={classes.alertError}>Credenciales incorrectas</Alert>
          }
          <form
            className={classes.form}
            noValidate
            onSubmit={handleSubmit(onSubmit)}
          >
            <Controller
              name="email"
              control={control}
              defaultValue={username}
              render={({ field }) => (
                <TextField
                variant="outlined"
                margin="normal"
                fullWidth
                id="email"
                required
                label="Nombre de usuario"
                autoComplete="email"
                autoFocus
                {...field}
                />
                )}
                />

            <Controller
              name="password"
              defaultValue={pass}
              control={control}
              render={({ field }) => (
                <TextField
                  variant="outlined"
                  margin="normal"
                  required
                  fullWidth
                  label="Contraseña"
                  type="password"
                  id="pass"
                  autoComplete="current-password"
                  {...field}
                />
              )}
            />

            <FormControlLabel
              control={<Checkbox value="remember" color="primary" />}
              label="Recordarme"
            />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              color="primary"
              className={classes.submit}
            >
              Ingresar
            </Button>
          </form>
        </div>
      </Grid>
    </Grid>
  );
};

export default Login;
