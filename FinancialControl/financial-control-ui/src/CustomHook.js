import React, {useState} from 'react';

const useStateForm = (initialState, callback) => {
   const [inputs, setInputs] = useState(initialState); 

   const handleSubmit = (event) => {
       if (event) {
           event.preventDefault();
        }
        callback(inputs);
   }

   const handleInputChange = (event) => {
       event.persist();
       let value;
       switch(event.target.type) {
           case 'number':
                value = Number(event.target.value);
                break;
            case 'date':
                value = new Date(event.target.value);
                break;
            default:
                value = event.target.value;
       }
       setInputs(inputs => ({...inputs, [event.target.name] : value }));
   }
   return {
       handleSubmit,
       handleInputChange,
       inputs,
       setInputs
   };
}

export default useStateForm;