import React, {useContext, createContext, useReducer} from "react";
import { reducerToast } from "../reducers/toastReducer";

export const ToastContext = createContext();

const initialState = {
        Type: '',
        Show: false,
        TimeMs: 5000,
        Title: '',
        Message: ''
};

export const ToastProvider = props => {
    
    const [state, dispatch] = useReducer(reducerToast, initialState);

    function showToast({type, show, title, message}) {
        dispatch({Type: 'type', Payload: type});
        dispatch({Type: 'show', Payload: show});
        dispatch({Type: 'title', Payload: title});
        dispatch({Type: 'message', Payload: message});
    }

    return (
        <ToastContext.Provider value={[state, dispatch, showToast]} >
            {props.children}
        </ToastContext.Provider>
    );
}

export const useToastContext = () => useContext(ToastContext);