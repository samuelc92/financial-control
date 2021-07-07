import React, { useEffect, useState } from 'react';
import { Link, useHistory, useParams } from 'react-router-dom';
import useStateForm from '../../CustomHook';
import expenseService from '../../services/expenseService';
import { makeStyles } from '@material-ui/core/styles';
import Container from '@material-ui/core/Container';
import Grid from '@material-ui/core/Grid';
import PageHeader from '../shared/PageHeader';
import TextField from '@material-ui/core/TextField'
import InputLabel from '@material-ui/core/InputLabel';
import Select from '@material-ui/core/Select';
import MenuItem from '@material-ui/core/MenuItem';
import FormControl from '@material-ui/core/FormControl';
import InputAdornment from '@material-ui/core/InputAdornment';
import { Button } from "@material-ui/core";
import 'date-fns';
import DateFnsUtils from '@date-io/date-fns';
import {
  MuiPickersUtilsProvider,
  KeyboardDatePicker,
} from '@material-ui/pickers';

const useStyles = makeStyles((theme) => ({
    formControl: {
        minWidth: 400
    }
}))
function CreateExpenseForm() {

    const classes = useStyles();
    let { expenseId } = useParams();
    let initialState = {
        category: 'DELIVERY',
        amount: 0,
        description: '',
        status: 'PAID',
        transactionDate: new Date()
    };

    const post = (inputs) => {
        expenseService
        .postExpense(inputs)
        .then ((response) => {
            alert('Cadastrado com sucesso!');
        })
        .catch((e) => { 
            alert('Error ao cadastrar uma despesa');
            console.log(e);
        })
    };

    const put = (inputs) => {
        expenseService
        .putExpense(inputs)
        .then ((response) => {
            alert('Atualizado com sucesso!');
            history.push("/expense")
        })
        .catch((e) => { 
            alert('Error ao atualizar uma despesa');
            console.log(e);
        })
    };

    const {setInputs, inputs, handleInputChange, handleSubmit} = useStateForm(initialState, expenseId ? put : post);
    const [isFetched, setIsfetched] = useState(false);
    let history = useHistory();

    useEffect(() => {
        if (!isFetched && expenseId) {
            expenseService
            .getExpenseById(expenseId)
            .then((response) => {
                setInputs(response.data[0]);
            })
            .catch((e) => {
                console.log(e);
            })
        }
        setIsfetched(true);

        return () => { setIsfetched(false) };
    });

    return (
            <Container>
                <PageHeader text="Create Expense" />
                <Grid container spacing={3}>
                    <Grid item xs={12} sm={4}>
                        <FormControl variant="outlined" className={classes.formControl}>
                            <InputLabel id="categoryLabel" name="categoryLabel">Category</InputLabel>
                            <Select id="category" name="category" value={inputs.category} label="Category" onChange={handleInputChange}>
                                <MenuItem value="BILLS">Bills</MenuItem>
                                <MenuItem value="DELIVERY">Delivery</MenuItem>
                                <MenuItem value="PUB">Pub</MenuItem>
                                <MenuItem value="SHOP">Shop</MenuItem>
                                <MenuItem value="SUPERMARKET">Supermarket</MenuItem>
                                <MenuItem value="UBER">Uber</MenuItem>
                                <MenuItem value="OTHER">Other</MenuItem>

                            </Select>
                        </FormControl>
                    </Grid>
                    <Grid item xs={12} sm={8}>
                        <TextField id="description" name="description" label="Description" variant="outlined" fullWidth onChange={handleInputChange} value={inputs.description} />
                    </Grid>
                    <Grid item xs={12} sm={4}>
                        <TextField 
                        id="amount" 
                        name="amount" 
                        label="Amount" 
                        variant="outlined" 
                        type="number"
                        InputProps={{startAdornment: <InputAdornment position="start">€</InputAdornment>,}}
                        fullWidth 
                        onChange={handleInputChange} 
                        value={inputs.amount} />
                    </Grid>
                    <Grid item xs={12} sm={3}>
                        <MuiPickersUtilsProvider utils={DateFnsUtils}>
                            <KeyboardDatePicker
                                disableToolbar
                                variant="inline"
                                format="dd/MM/yyyy"
                                margin="none"
                                id="transactionDate"
                                name="transactionDate"
                                label="Transaction Date"
                                value={inputs.transactionDate}
                                onChange={(event) => {console.log(event); setInputs({...inputs, ['transactionDate'] : event})}}
                                KeyboardButtonProps={{
                                    'aria-label': 'change date',
                                }}
                            />
                        </MuiPickersUtilsProvider>
                    </Grid>
                    <Grid item xs={12} sm={1}>
                        <Button variant="contained" size="medium" color="primary" onClick={handleSubmit}>
                            Save
                        </Button>
                    </Grid>
                    <Grid item xs={12} sm={1}>
                        <Link to="/expense">
                            <Button variant="contained" size="medium">
                               Cancel 
                            </Button>
                        </Link>
                    </Grid>
                </Grid>
            </Container>
    )
}
export default CreateExpenseForm;