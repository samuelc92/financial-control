//import logo from './logo.svg';
import './App.css';
import { BrowserRouter as Router, Route, Link, Switch } from "react-router-dom";
import CreateExpenseForm from './components/expenses/CreateExpenseForm';
import { Navbar, Nav } from 'react-bootstrap';
import ListExpenseForm from './components/expenses/ListExpenseForm';

function App() {
  return (
    <div className="App">
      <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
      <Router>
        <main>
          <Navbar bg="dark" variant="dark">
            <Navbar.Brand>Financial Control</Navbar.Brand>
            <Nav className="mr-auto">
              <Link to="/expense">Expenses</Link>
            </Nav>
          </Navbar>
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