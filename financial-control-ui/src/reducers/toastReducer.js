export const reducerToast = (state, action) => {
    switch(action.Type) {
        case 'type':
            return({...state, Type: action.Payload});
        case 'show':
            return({...state, Show: action.Payload});        
        case 'timems':
            return({...state, TimeMs: action.Payload});
        case 'title':
            return({...state, Title: action.Payload});
        case 'message':
            return({...state, Message: action.Payload});
        default:
            return state;
    }
}