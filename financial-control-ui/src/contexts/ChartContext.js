import React, {useContext, createContext, useReducer} from "react";
import { chartReducer } from "../reducers/chartReducer";

export const ChartContext = createContext();

const initialValue = {
  labels: ['Red', 'Blue', 'Yellow', 'Green'],
  data: [12, 19, 3, 5],
};
/*const initialValue = {
  labels: ['Red', 'Blue', 'Yellow', 'Green'],
  datasets: [
    {
      label: '# of Votes',
      data: [12, 19, 3, 5],
      backgroundColor: [
        'rgba(255, 99, 132, 0.2)',
        'rgba(54, 162, 235, 0.2)',
        'rgba(255, 206, 86, 0.2)',
        'rgba(75, 192, 192, 0.2)',
        'rgba(153, 102, 255, 0.2)',
        'rgba(255, 159, 64, 0.2)',
      ],
      borderColor: [
        'rgba(255, 99, 132, 1)',
        'rgba(54, 162, 235, 1)',
        'rgba(255, 206, 86, 1)',
        'rgba(75, 192, 192, 1)',
        'rgba(153, 102, 255, 1)',
        'rgba(255, 159, 64, 1)',
      ],
      borderWidth: 1,
    },
  ],
};
*/
export const ChartProvider = props => {

    const [state, dispatch] = useReducer(chartReducer, initialValue);
    
    return (
        <ChartProvider.Provider value={[state, dispatch]}>
            {props.children}
        </ChartProvider.Provider>
    );
}

export const useChartContext = () => useContext(ChartContext);