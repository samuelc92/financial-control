import React, { useState } from 'react';
import Snackbar from '@material-ui/core/Snackbar';
import MuiAlert from '@material-ui/lab/Alert';
import { makeStyles } from '@material-ui/core/styles';
import { useToastContext } from '../../contexts/ToastContext';

function Alert(props) {
  return <MuiAlert elevation={6} variant="filled" {...props} />;
}

const useStyles = makeStyles((theme) => ({
  root: {
    width: '50%',
    '& > * + *': {
      marginTop: theme.spacing(2),
    },
  },
}));

export default function CustomizedToast({severity, text, opened}) {

    const classes = useStyles();
    const [state, reducer, showToast] = useToastContext ();
    const [position, setPosition] = useState({

      vertical: 'top',
      horizontal: 'right'
    })

    const { vertical, horizontal } = position;

    const handleClose = (event, reason) => {
        if (reason === 'clickaway')
            return;
        //setState({ ...state, open: false});
    }

    console.log(state.Show);

    return (
        <div className={classes.root}>
            <Snackbar 
              open={state.Show} 
              autoHideDuration={1000} 
              anchorOrigin={{ vertical, horizontal }}
              onClose={handleClose}>
                <Alert onClose={handleClose} severity="success">
                This is a success message!
                </Alert>
              <Alert severity={state.Type} variant="filled">{state.Message}</Alert>
            </Snackbar>
        </div>
    );
}