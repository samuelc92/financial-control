import React, { useEffect, useState } from 'react';
import { Pie } from 'react-chartjs-2';

let initialData = {
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

function PieChart({labels, values}) {

  const [pieData, setPieData] = useState(initialData);
  const [fetched, setFetched] = useState(false);

  useEffect(() => {
    if (!fetched) {
      setPieData({...pieData, labels: labels, datasets: [{...initialData.datasets[0], data: values}]});
    }
    setFetched(true);
    return () => {
      setFetched(false);
    }
  });

  return (
      <div>
          <Pie 
          data={pieData} 
          width={1920}
          height={300}
          options={{ 
            title: {
              display: true,
              text: 'Expenses by Category',
              fontSize: 20
            },
            legend: {
              display: true,
              position: 'right'
            },
            maintainAspectRatio: false 
          }}
          />
      </div>
  )
};

export default PieChart;