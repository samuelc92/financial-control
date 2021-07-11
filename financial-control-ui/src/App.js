import './App.css';
import { ToastProvider } from './contexts/ToastContext';
import CustomizedToast from './components/shared/CustomizedToast';
import Routes from './Routes';

/*const useStyles = makeStyles((theme) => ({
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
*/

function App() {
  return (
    <div>
      <link rel="stylesheet" href="https://fonts.googleapis.com/icon?family=Material+Icons" />
      <ToastProvider>
        <CustomizedToast></CustomizedToast>
        <Routes />
      </ToastProvider>
    </div>
  );
}

export default App;