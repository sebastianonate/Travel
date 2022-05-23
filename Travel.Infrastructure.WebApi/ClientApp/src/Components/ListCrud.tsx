import React, { createElement, FC } from "react";
import { createStyles, makeStyles, Theme } from "@material-ui/core/styles";
import Paper from "@material-ui/core/Paper";
import Table from "@material-ui/core/Table";
import TableBody from "@material-ui/core/TableBody";
import TableCell from "@material-ui/core/TableCell";
import TableContainer from "@material-ui/core/TableContainer";
import TableHead from "@material-ui/core/TableHead";
import TablePagination from "@material-ui/core/TablePagination";
import TableRow from "@material-ui/core/TableRow";
import { Button, Container, Grid, Modal } from "@material-ui/core";
import { useDispatch } from "react-redux";

const useStyles = makeStyles((theme: Theme) =>
  createStyles({
    root: {
      width: "100%",
    },
    container: {
      maxHeight: 800,
    },
    title: {
      color: theme.palette.success.dark,
    },
  })
);

interface Column {
  id: string;
  label: string;
  minWidth?: number;
  align?: "right";
  format?: (value: number) => string;
  optionsRender?: (value: any) => JSX.Element;
  optionsArray?: (value: any) => JSX.Element;
}

interface ListCrudInterface {
  columns: Column[];
  rows: any[] | undefined;
  handleChangePage: any;
  handleChangeRowsPerPage: any;
  title: string;
  modalForm: JSX.Element;
  statusModalForm: any;
  handleModalForm: any;
  selectEntity: any;
}

const ListCrud: React.SFC<ListCrudInterface> = ({
  columns,
  rows,
  title,
  modalForm,
  statusModalForm,
  handleModalForm,
  selectEntity,
}) => {
  const classes = useStyles();
  const dispatch = useDispatch();

  return (
    <>
      <Container fixed>
        <h1 className={classes.title}>{title}</h1>
        <Button variant="contained" color="secondary" onClick={() => dispatch(handleModalForm(true))}>Nuevo</Button>
        <Grid container>
          <Grid item md={12}>
            <Paper className={classes.root}>
              <TableContainer className={classes.container}>
                <Table stickyHeader aria-label="sticky table">
                  <TableHead>
                    <TableRow>
                      {columns.map((column) => (
                        <TableCell
                          key={column.id}
                          align={column.align}
                          style={{ minWidth: column.minWidth }}
                        >
                          {column.label}
                        </TableCell>
                      ))}
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {rows?.map((row) => {
                      return (
                        <TableRow
                          hover
                          role="checkbox"
                          tabIndex={-1}
                          key={row.code}
                        >
                          {columns.map((column) => {
                            const ids = column.id.split(".");
                            let value = row;
                            ids.forEach((id) => {
                              if (value) value = value[id];
                            });
                            return (
                              <TableCell key={column.id} align={column.align}>
                                {column.format && typeof value === "number"
                                  ? column.format(value)
                                  : column.optionsRender
                                  ? column.optionsRender(row["id"])
                                  : column.optionsArray
                                  ? column.optionsArray(value)
                                  : value}
                              </TableCell>
                            );
                          })}
                        </TableRow>
                      );
                    })}
                  </TableBody>
                </Table>
              </TableContainer>
            </Paper>
          </Grid>
        </Grid>
        <Modal
          open={statusModalForm}
          onClose={() => {
            dispatch(handleModalForm(false));
            dispatch(selectEntity(null));
          }}
          aria-labelledby="simple-modal-title"
          aria-describedby="simple-modal-description"
        >
          {modalForm}
        </Modal>
      </Container>
    </>
  );
};

export default ListCrud;
