//import logo from './logo.svg';
import './App.css';
import { makeStyles } from '@material-ui/core/styles';
import { BrowserRouter as Router, Route, Link, Switch } from "react-router-dom";
import CreateExpenseForm from './components/expenses/CreateExpenseForm';
import ListExpenseForm from './components/expenses/ListExpenseForm';
import { AppBar, Typography } from '@material-ui/core';
import { IconButton } from '@material-ui/core';
import Toolbar from '@material-ui/core/Toolbar';
import MenuIcon from '@material-ui/icons/Menu';

const useStyles = makeStyles((theme) => ({
  root: {
    flexGrow: 1,
  },
  menuButton: {
    marginRight: theme.spacing(2),
  },
  title: {
    flexGrow: 1,
  },
}));

function App() {
  const classes = useStyles();
  return (
    <div className="App">
      <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
      <Router>
        <main>
          <AppBar position="static">
            <Toolbar>
              <IconButton edge="start" className={classes.menuButton} color="inherit" aria-label="menu">
                <MenuIcon />
              </IconButton>
              <Typography>
                <Link to="/expense">Expenses</Link>
              </Typography>
            </Toolbar>
          </AppBar>
          <Switch>
            <Route path="/expense" exact component={ListExpenseForm} />
            <Route path="/expense/create" exact component={CreateExpenseForm} />
            <Route path="/expense/create/:expenseId" component={CreateExpenseForm} />
          </Switch>
        </main>
      </Router>
    </div>
  );
}

export default App;

      /*

       <li><Link to={`/about/${name}`}>About</Link></li>
       <Route path="/about/:name"  component={About} />
        <header className="App-header">
        <img src={logo} className="App-logo" alt="logo" />
        <p>
          Edit <code>src/App.js</code> and save to reload.
        </p>
        <a
          className="App-link"
          href="https://reactjs.org"
          target="_blank"
          rel="noopener noreferrer"
        >
          Learn React
        </a>
      </header>

      */ 