export const chartReducer = (state, action) => {
    switch(action.Type) {
        case 'labels':
            return ({...state, labels: action.Payload});
        case 'data': {
            let {data} = state.datasets[0];
            data = action.Payload;
            console.log("Data: ")
            console.log(data);
            return ({...state, datasets: action.Payload});
        }
        default:
            return state;
    }
}