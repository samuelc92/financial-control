import React, { useState, useEffect } from "react";
import { Link } from "react-router-dom";
import expenseService from "../../services/expenseService";
import clsx from 'clsx';
import { makeStyles } from "@material-ui/core/styles";
import TableContainer from "@material-ui/core/TableContainer";
import Table from "@material-ui/core/Table";
import Paper from "@material-ui/core/Paper";
import TableHead from "@material-ui/core/TableHead";
import TableRow from "@material-ui/core/TableRow";
import TableCell from "@material-ui/core/TableCell";
import TableBody from "@material-ui/core/TableBody";
import TablePagination from "@material-ui/core/TablePagination";
import Checkbox from '@material-ui/core/Checkbox';
import { lighten, Typography } from "@material-ui/core";
import Toolbar from '@material-ui/core/Toolbar';
import Tooltip from '@material-ui/core/Tooltip';
import IconButton from '@material-ui/core/IconButton';
import DeleteIcon from '@material-ui/icons/Delete';
import FilterListIcon from '@material-ui/icons/FilterList';
import Icon from '@material-ui/core/Icon';
import { useToastContext } from "../../contexts/ToastContext";

const columns = [
  { id: "category", label: "Category", minWidth: 170 },
  { id: "description", label: "Description", minWidth: 100 },
  {
    id: "amount",
    label: "Amount",
    minWidth: 170,
    align: "right",
    format: (value) => value.toLocaleString("en-US"),
  },
  {
    id: "transactionDate",
    label: "TransactionDate",
    minWidth: 170,
    align: "right",
  },
];

const useStyles = makeStyles((theme) => ({
  root: {
    width: "100%",
    flexGrow: 1,
  },
  container: {
    maxHeight: "100",
  },
  buttons: {
    margin: theme.spacing(1)
  }
}));

const useToolbarStyles = makeStyles((theme) => ({
  root: {
    paddingLeft: theme.spacing(2),
    paddingRight: theme.spacing(1),
  },
  highlight:
    theme.palette.type === 'light'
      ? {
          color: theme.palette.secondary.main,
          backgroundColor: lighten(theme.palette.secondary.light, 0.85),
        }
      : {
          color: theme.palette.text.primary,
          backgroundColor: theme.palette.secondary.dark,
        },
  title: {
    flex: '1 1 100%',
  },
}));


export default function ListExpenseForm() {

  const classes = useStyles();
  const toolbarClasses = useToolbarStyles();
  const [page, setPage] = useState(0);
  const [rowsPerPage, setRowsPerPage] = useState(10);
  const [isFetched, setIsfetched] = useState(false);
  const [expenses, setExpenses] = useState([]);
  const [selected, setSelected] = useState([]);
  const [toastState, toastDispatch, showToast] = useToastContext();

  const handleSelectAllClick = (event) => {
    if (!event.target.checked) {
      setSelected([]);
      return;
    }
    const newSelecteds = expenses.map((n) => n.id);
    setSelected(newSelecteds);
  }

  const handleClick = (event, id) => {
    const index = selected.indexOf(id);
    let newSelected = [];

    if (index === -1) {
      newSelected = newSelected.concat(selected, id);
    } else if (index === 0) {
      newSelected = newSelected.concat(selected.slice(1));
    } else if (index === selected.length - 1) {
      newSelected = newSelected.concat(selected.slice(0,-1));
    } else if (index > 0) {
      newSelected = newSelected.concat(
        selected.slice(0, index),
        selected.slice(index + 1)
      );
    }
    setSelected(newSelected);
  }

  const isSelected = id => selected.indexOf(id) !== -1;

  const handleChangePage = (event, newPage) => {
    setPage(newPage);
  };

  const handleChangeRowsPerPage = (event) => {
    setRowsPerPage(+event.target.value);
    setPage(0);
  };

  const handleDeleteExpense = (selected) => {
    if (selected && selected.length > 0) {
      expenseService
      .deleteExpense(selected)
      .then(() => {
        showToast({type: 'success', show: true, title: 'Expense Deleted Successfull', message: 'Expense has been deleted.'});
      })
      .catch((e) => {
        console.log(e);
        showToast({type: 'error', show: true, title: 'Expense Deleted Error', message: 'Error on deleting an expense.'});
      })
    }
  };

  useEffect(() => {
    if (!isFetched) {
        expenseService
        .getExpenses()
        .then((response) => {
            setExpenses(response.data);
        })
        .catch((e) => {
          console.log(e);
        })
    }
    setIsfetched(true);

    return () => {
      setIsfetched(false);
    };
  });


  return (
    <Paper className={classes.root}>
      <Toolbar className={clsx(classes.root, { [toolbarClasses.highlight]: selected.length > 0 })}>
        {selected.length > 0 ? (
          <Typography className={toolbarClasses.title} color="inherit" variant="subtitle1" component="div">
            {selected.length} selected
          </Typography>
        ) : (
          <Typography className={toolbarClasses.title} variant="h6" id="tableTitle" component="div">
            Expenses
          </Typography>
        )}

        <Tooltip title="Add">
          <Link to="/expense/create">
            <IconButton aria-label="add_circle">
              <Icon color="primary">add_circle</Icon>
            </IconButton>
          </Link>
        </Tooltip>

        {selected.length > 0 ? (
          <Tooltip title="Delete">
            <IconButton aria-label="delete">
              <DeleteIcon onClick={() => handleDeleteExpense(selected)}/>
            </IconButton>
          </Tooltip>
        ) : (
          <Tooltip title="Filter list">
            <IconButton aria-label="filter list">
              <FilterListIcon />
            </IconButton>
          </Tooltip>
        )}
      </Toolbar>
      <TableContainer className={classes.container}>
        <Table stickyHeader aria-label="sticky table">
          <TableHead>
            <TableRow>
              <TableCell padding="checkbox">
                <Checkbox
                  indeterminate={selected.length > 0 && selected.length < expenses.length}
                  checked={expenses.length > 0 && selected.length === expenses.length}
                  onChange={handleSelectAllClick}
                  inputProps={{ 'aria-label': 'select all desserts' }}
              />
              </TableCell>
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
            {expenses
              .slice(page * rowsPerPage, page * rowsPerPage + rowsPerPage)
              .map((row) => {
                const isItemSelected = isSelected(row.id);
                return (
                  <TableRow 
                    hover 
                    role="checkbox" 
                    onClick={(event) => handleClick(event, row.id)}
                    tabIndex={-1} 
                    key={row.id}>
                    <TableCell padding="checkbox">
                      <Checkbox
                        checked={isItemSelected}
                      />
                    </TableCell>
                    {columns.map((column) => {
                      const value = row[column.id];
                      return (
                        <TableCell key={column.id} align={column.align}>{column.format && typeof value === 'number' ? column.format(value) :
                        value ? value.toString() : "" }</TableCell>
                      )
                    })}
                  </TableRow>
                );
              })}
          </TableBody>
        </Table>
      </TableContainer>
      <TablePagination
        rowsPerPageOptions={[10, 25, 100]}
        component="div"
        count={expenses.length}
        rowsPerPage={rowsPerPage}
        page={page}
        onChangePage={handleChangePage}
        onChangeRowsPerPage={handleChangeRowsPerPage}
      />
    </Paper>
  );
}