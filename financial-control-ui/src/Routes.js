import { Component } from 'react';
import { BrowserRouter, Route, Switch, Redirect, useHistory } from "react-router-dom";
import CreateExpenseForm from './components/expenses/CreateExpenseForm';
import ListExpenseForm from './components/expenses/ListExpenseForm';
import Menu from './components/shared/Menu';

const PrivateRoute = ({ component: Component, ...rest}) => ( 
    <Route
        {...rest}
        render = {
            props => {
                return (
                    <>
                        <Menu />
                        <Component {...props} />
                    </>
                );
            }
        }
   /> 
);

const Routes = () => {
    return (
        <BrowserRouter>
            <Switch>
              <PrivateRoute path="/expense" exact={true} component={ListExpenseForm} />
              <PrivateRoute path="/expense/create" exact={true} component={CreateExpenseForm} />
              <PrivateRoute path="/expense/create/:expenseId" component={CreateExpenseForm} />
            </Switch>
        </BrowserRouter>
    )
}
export default Routes;