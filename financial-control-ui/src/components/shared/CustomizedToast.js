import React, { useState } from 'react';
import Snackbar from '@material-ui/core/Snackbar';
import {Alert, AlertTitle} from '@material-ui/lab';
import { useToastContext } from '../../contexts/ToastContext';

export default function CustomizedToast({severity, text, opened}) {

    const [state, reducer, showToast] = useToastContext();
    const [position, setPosition] = useState({

      vertical: 'top',
      horizontal: 'right'
    })

    const { vertical, horizontal } = position;

    const handleClose = (event, reason) => {
        if (reason === 'clickaway')
            return;
        showToast({type: '', show: false, title: '', message: ''});
    }

    return (
      <Snackbar 
        open={state.Show} 
        autoHideDuration={3000} 
        anchorOrigin={{ vertical, horizontal }}
        onClose={handleClose}
        >
          <Alert severity={state.Type} variant="filled">
            {state.Message}
          </Alert>
      </Snackbar>
    );
}